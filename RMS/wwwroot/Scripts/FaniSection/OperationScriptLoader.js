/* Loads operation-specific scripts only when a tree operation is selected.
   Scripts are loaded sequentially and cached for the lifetime of the page. */
(function (window, document) {
    "use strict";

    // Prefer the URL explicitly supplied by the Razor view. This also works
    // when the partial is injected and document.currentScript is unavailable.
    var scriptBaseUrl = window.OperationScriptsBaseUrl || "";
    var loaderScript = document.currentScript;

    // Fallback: locate the loader tag by its own URL, not by a custom attribute.
    if (!scriptBaseUrl) {
        var loaderScripts = document.getElementsByTagName("script");
        for (var i = loaderScripts.length - 1; i >= 0; i--) {
            var source = loaderScripts[i].src || "";
            if (source.indexOf("OperationScriptLoader.js") >= 0) {
                loaderScript = loaderScripts[i];
                break;
            }
        }

        if (loaderScript && loaderScript.src) {
            scriptBaseUrl = loaderScript.src.substring(0, loaderScript.src.lastIndexOf("/") + 1);
        }
    }

    if (scriptBaseUrl && scriptBaseUrl.charAt(scriptBaseUrl.length - 1) !== "/") {
        scriptBaseUrl += "/";
    }

    if (!scriptBaseUrl) {
        console.error("OperationScriptLoader: base URL could not be resolved. Check the data-base-url attribute on OperationScriptLoader.js.");
    }

    var loadedScripts = Object.create(null);

    var coreScripts = [
        "AssisScript.js",
        "AssisScript2.js",
        "CheckConditionContextRelated.js"
    ];

    var featureScripts = {
        paykani: [
            "PayKaniNew.js",
            "PayKaniEzafeBahaNew.js"
        ],
        chanelkani: [
            "ChanelKaniNew.js",
            "ChanelKaniEzafeBahaNew.js"
        ],
        khakbardari: [
            "AmalyateKhaki.js",
            "AmalyateKhaki2.js",
            "AmalyateKhakiSumMaghadir.js",
            "AmalyateKhakiEzafeBaha.js"
        ],
        gharzeh: [
            "AmalyateKhakiGharzeh.js"
        ],
        khakrizi: [
            "KhakRizi.js",
            "KhakRiziEdit.js"
        ],
        abniefani: [
            "AssisAbnieFani.js",
            "AbnieFani.js",
            "AbnieFaniShowRizMetre.js",
            "DrawingPol.js",
            "DrawingPlan2.js"
        ],
        board: [
            "Board.js"
        ],
        boardstand: [
            "BoardStand.js"
        ],
        zaribbala: [
            "ZaribBalaSari.js"
        ],
        zaribmanteghe: [
            "ZaribManteghe.js"
        ]
    };

    function getFeatureScripts(functionName) {
        var name = (functionName || "").toLowerCase();

        if (name.indexOf("paykani") >= 0) return featureScripts.paykani;
        if (name.indexOf("chanelkani") >= 0) return featureScripts.chanelkani;
        if (name.indexOf("khakbardari") >= 0 || name.indexOf("rizeshbardari") >= 0)
            return featureScripts.khakbardari;
        if (name.indexOf("gharzeh") >= 0) return featureScripts.gharzeh;
        if (name.indexOf("khakrizi") >= 0) return featureScripts.khakrizi;
        if (name.indexOf("abniefani") >= 0 || name.indexOf("drawingpol") >= 0 ||
            name.indexOf("drawingplan") >= 0 || name === "pol" || name.indexOf("showexistingpol") >= 0)
            return featureScripts.abniefani;
        if (name.indexOf("boardstand") >= 0) return featureScripts.boardstand;
        if (name.indexOf("board") >= 0) return featureScripts.board;
        if (name.indexOf("zaribbala") >= 0) return featureScripts.zaribbala;
        if (name.indexOf("zaribmanteghe") >= 0 || name.indexOf("zaribmantaghe") >= 0)
            return featureScripts.zaribmanteghe;

        return [];
    }

    function loadScript(fileName) {
        if (loadedScripts[fileName]) return loadedScripts[fileName];

        loadedScripts[fileName] = new Promise(function (resolve, reject) {
            var script = document.createElement("script");
            script.src = scriptBaseUrl + fileName;
            script.async = false;
            script.onload = function () {
                resolve();
            };
            script.onerror = function () {
                delete loadedScripts[fileName];
                reject(new Error("Unable to load script: " + script.src));
            };
            (document.head || document.getElementsByTagName("head")[0]).appendChild(script);
        });

        return loadedScripts[fileName];
    }

    function loadSequentially(fileNames, index) {
        if (index >= fileNames.length) return Promise.resolve();
        return loadScript(fileNames[index]).then(function () {
            return loadSequentially(fileNames, index + 1);
        });
    }

    function getFunctionName(inlineCall) {
        var match = /^\s*([A-Za-z_$][\w$]*)\s*\(/.exec(inlineCall || "");
        return match ? match[1] : "";
    }

    window.OperationScriptLoader = {
        ensureForCall: function (inlineCall) {
            var functionName = getFunctionName(inlineCall);
            var requiredScripts = coreScripts.concat(getFeatureScripts(functionName));

            return loadSequentially(requiredScripts, 0).then(function () {
                if (functionName && typeof window[functionName] !== "function") {
                    throw new Error("Operation function was not found after loading scripts: " + functionName);
                }
                return functionName;
            });
        }
    };
})(window, document);

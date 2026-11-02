function convertPersianToEnglish(str) {
    if (!str) return str;
    // تبدیل ارقام فارسی و عربی به انگلیسی
    const persianDigits = ['۰', '۱', '۲', '۳', '۴', '۵', '۶', '۷', '۸', '۹'];
    const arabicDigits = ['٠', '١', '٢', '٣', '٤', '٥', '٦', '٧', '٨', '٩'];
    let result = str;
    for (let i = 0; i < 10; i++) {
        result = result.replace(new RegExp(persianDigits[i], 'g'), i)
            .replace(new RegExp(arabicDigits[i], 'g'), i);
    }
    // تبدیل جداکننده اعشار فارسی یا عربی به "."
    result = result.replace(/[٫٬]/g, '.');
    return result;
}


function toPersianDigits(num) {
    if (num == null) return '';
    return num.toString().replace(/\d/g, d => '۰۱۲۳۴۵۶۷۸۹'[d]);
}

function convertNumbersInPage() {
    $('*').each(function () {
        if (this.children.length === 0 && this.nodeType === 1) {
            const originalText = $(this).text();
            const convertedText = toPersianDigits(originalText);
            if (originalText !== convertedText) {
                $(this).text(convertedText);
            }
        }
    });
}


function ShowWaiting() {

    if ($('#ContainerWaiting').length) {
        return '';
    }

    return `
        <div id="ContainerWaiting" style="
            width: 100%;
            height: 100%;
            position: absolute;
            background-color: #fff;
            z-index: 99;
            opacity: 0.7;
            display: flex;
            justify-content: center;
            align-items: center;">

            <div style="
                width: 40px;
                height: 40px;
                border: 5px solid #ddd;
                border-top: 5px solid #3498db;
                border-radius: 50%;
                animation: spin 1s linear infinite;">
            </div>

        </div>

        <style>
            @keyframes spin {
                0% {
                    transform: rotate(0deg);
                }
                100% {
                    transform: rotate(360deg);
                }
            }
        </style>
    `;
}

function RemoveExtraDecimalZeros(value) {
    if (value === null || value === undefined || value.toString().trim() === '')
        return '';

    return Number(parseFloat(value));
}
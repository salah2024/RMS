export default class BridgeRenderer {

    constructor(svgId) {

        this.svg = document.getElementById(svgId);

        this.ns = "http://www.w3.org/2000/svg";

        this.layers = {};

        this.createLayers();

    }

    createLayers() {

        this.svg.innerHTML = "";

        this.layers.shadow = this.group("shadow");

        this.layers.bridge = this.group("bridge");

        this.layers.dimensions = this.group("dimensions");

        this.layers.text = this.group("text");

    }

    group(name) {

        const g = document.createElementNS(this.ns, "g");

        g.setAttribute("id", name);

        this.svg.appendChild(g);

        return g;

    }

    clear() {

        this.createLayers();

    }

    line(x1, y1, x2, y2, style = {}) {

        const l = document.createElementNS(this.ns, "line");

        l.setAttribute("x1", x1);

        l.setAttribute("y1", y1);

        l.setAttribute("x2", x2);

        l.setAttribute("y2", y2);

        l.setAttribute("stroke", style.color || "#263238");

        l.setAttribute("stroke-width", style.width || 2);

        l.setAttribute("stroke-linecap", "round");

        l.setAttribute("stroke-linejoin", "round");

        this.layers.bridge.appendChild(l);

        return l;

    }

    rect(x, y, w, h, style = {}) {

        const r = document.createElementNS(this.ns, "rect");

        r.setAttribute("x", x);

        r.setAttribute("y", y);

        r.setAttribute("width", w);

        r.setAttribute("height", h);

        r.setAttribute("fill", style.fill || "none");

        r.setAttribute("stroke", style.color || "#263238");

        r.setAttribute("stroke-width", style.width || 2);

        this.layers.bridge.appendChild(r);

        return r;

    }

    polyline(points, style = {}) {

        const p = document.createElementNS(this.ns, "polyline");

        p.setAttribute("points", points);

        p.setAttribute("fill", "none");

        p.setAttribute("stroke", style.color || "#263238");

        p.setAttribute("stroke-width", style.width || 2);

        this.layers.bridge.appendChild(p);

        return p;

    }

    polygon(points, style = {}) {

        const p = document.createElementNS(this.ns, "polygon");

        p.setAttribute("points", points);

        p.setAttribute("fill", style.fill || "#eceff1");

        p.setAttribute("stroke", style.color || "#263238");

        p.setAttribute("stroke-width", style.width || 2);

        this.layers.bridge.appendChild(p);

        return p;

    }

    circle(x, y, r, style = {}) {

        const c = document.createElementNS(this.ns, "circle");

        c.setAttribute("cx", x);

        c.setAttribute("cy", y);

        c.setAttribute("r", r);

        c.setAttribute("fill", style.fill || "#1976d2");

        c.setAttribute("stroke", style.color || "#0d47a1");

        c.setAttribute("stroke-width", style.width || 2);

        this.layers.bridge.appendChild(c);

        return c;

    }

    text(x, y, value, style = {}) {

        const t = document.createElementNS(this.ns, "text");

        t.setAttribute("x", x);

        t.setAttribute("y", y);

        t.setAttribute("font-size", style.size || 18);

        t.setAttribute("font-family", "IRANSans");

        t.setAttribute("fill", style.color || "#1565C0");

        t.textContent = value;

        this.layers.text.appendChild(t);

        return t;

    }

    dimensionHorizontal(x1, x2, y, value) {

        this.line(x1, y, x2, y, {

            color: "#9E9E9E",

            width: 1

        });

        this.text((x1 + x2) / 2, y - 5, value);

    }

    dimensionVertical(x, y1, y2, value) {

        this.line(x, y1, x, y2, {

            color: "#9E9E9E",

            width: 1

        });

        this.text(x + 8, (y1 + y2) / 2, value);

    }

    autoFit(padding = 40) {

        const box = this.svg.getBBox();

        this.svg.setAttribute(

            "viewBox",

            `${box.x - padding}
             ${box.y - padding}
             ${box.width + padding * 2}
             ${box.height + padding * 2}`

        );

    }

}
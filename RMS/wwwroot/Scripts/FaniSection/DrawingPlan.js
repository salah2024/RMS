function DrawPlan(alfa, D, L, w1, w2, w3, w4, b2, c1, c2, j, alfaw1, alfaw2, alfaw3, alfaw4, StartPointX, StartPointY, Scale, Meyar) {
    debugger
    D *= Scale; L *= Scale; w1 *= Scale; w2 *= Scale; w3 *= Scale; w4 *= Scale;
    b2 *= Scale; c1 *= Scale; c2 *= Scale;
    Tool = D + ((2 * c1) - (2 * j));
    linePlan(StartPointX, StartPointY - (c2 / Math.cos(toRadians(alfa))) - (c1 / Math.cos(toRadians(alfa))), StartPointX, StartPointY - (c1 / Math.cos(toRadians(alfa))) - (c2 / Math.cos(toRadians(alfa))) - 2 * D);////1

    linePlan(StartPointX, StartPointY, StartPointX, StartPointY - (c1 / Math.cos(toRadians(alfa))));////1
    linePlan(StartPointX, StartPointY - (c1 / Math.cos(toRadians(alfa))), StartPointX, StartPointY - (c1 / Math.cos(toRadians(alfa))) - (c2 / Math.cos(toRadians(alfa))));////1

    lineDashedPlan(StartPointX, StartPointY, StartPointX + (L * Math.cos(toRadians(alfa))), StartPointY + (L * Math.sin(toRadians(alfa))));////2
    linePlan(StartPointX, StartPointY - (c1 / Math.cos(toRadians(alfa))), StartPointX + (L * Math.cos(toRadians(alfa))), StartPointY - (c1 / Math.cos(toRadians(alfa))) + (L * Math.sin(toRadians(alfa))));////3
    linePlan(StartPointX, StartPointY - (c2 / Math.cos(toRadians(alfa))) - (c1 / Math.cos(toRadians(alfa))), StartPointX + (L * Math.cos(toRadians(alfa))), StartPointY - (c2 / Math.cos(toRadians(alfa))) - (c1 / Math.cos(toRadians(alfa))) + (L * Math.sin(toRadians(alfa))));////3
    linePlan(StartPointX, StartPointY, StartPointX, StartPointY + (D / Math.cos(toRadians(alfa))));////4
    lineDashedPlan(StartPointX, StartPointY + (D / Math.cos(toRadians(alfa))), StartPointX + (L * Math.cos(toRadians(alfa))), StartPointY + (D / Math.cos(toRadians(alfa))) + (L * Math.sin(toRadians(alfa))));////5
    linePlan(StartPointX, StartPointY + (D / Math.cos(toRadians(alfa))), StartPointX, StartPointY + (D / Math.cos(toRadians(alfa))) + (c1 / Math.cos(toRadians(alfa))));////6
    linePlan(StartPointX, StartPointY + (c1 / Math.cos(toRadians(alfa))) + (D / Math.cos(toRadians(alfa))), StartPointX, StartPointY + (D / Math.cos(toRadians(alfa))) + (c2 / Math.cos(toRadians(alfa))) + (c1 / Math.cos(toRadians(alfa))));////6

    linePlan(StartPointX, StartPointY + (D / Math.cos(toRadians(alfa))) + (c1 / Math.cos(toRadians(alfa))), StartPointX + (L * Math.cos(toRadians(alfa))), StartPointY + (D / Math.cos(toRadians(alfa))) + (c1 / Math.cos(toRadians(alfa))) + (L * Math.sin(toRadians(alfa))));////6
    linePlan(StartPointX, StartPointY + (D / Math.cos(toRadians(alfa))) + (c2 / Math.cos(toRadians(alfa))) + (c1 / Math.cos(toRadians(alfa))), StartPointX + (L * Math.cos(toRadians(alfa))), StartPointY + (D / Math.cos(toRadians(alfa))) + (c2 / Math.cos(toRadians(alfa))) + (c1 / Math.cos(toRadians(alfa))) + (L * Math.sin(toRadians(alfa))));////6


    linePlan(StartPointX, StartPointY + (c2 / Math.cos(toRadians(alfa))) + (c1 / Math.cos(toRadians(alfa))) + (D / Math.cos(toRadians(alfa))), StartPointX, StartPointY + (c1 / Math.cos(toRadians(alfa))) + (c2 / Math.cos(toRadians(alfa))) + (D / Math.cos(toRadians(alfa))) + 2 * D);////1
    //////////////////
    linePlan(StartPointX + (L * Math.cos(toRadians(alfa))), StartPointY + (L * Math.sin(toRadians(alfa))), StartPointX + (L * Math.cos(toRadians(alfa))), StartPointY + (L * Math.sin(toRadians(alfa))) - (c1 / Math.cos(toRadians(alfa))));////1
    linePlan(StartPointX + (L * Math.cos(toRadians(alfa))), StartPointY - (c1 / Math.cos(toRadians(alfa))) + (L * Math.sin(toRadians(alfa))), StartPointX + (L * Math.cos(toRadians(alfa))), StartPointY + (L * Math.sin(toRadians(alfa))) - (c1 / Math.cos(toRadians(alfa))) - (c2 / Math.cos(toRadians(alfa))));////1

    linePlan(StartPointX + (L * Math.cos(toRadians(alfa))), StartPointY - (c1 / Math.cos(toRadians(alfa))) + (L * Math.sin(toRadians(alfa))), StartPointX + (L * Math.cos(toRadians(alfa))), StartPointY + (L * Math.sin(toRadians(alfa))) - (c1 / Math.cos(toRadians(alfa))) - (c2 / Math.cos(toRadians(alfa))) - 2 * D);////1


    linePlan(StartPointX + (L * Math.cos(toRadians(alfa))), StartPointY + (L * Math.sin(toRadians(alfa))), StartPointX + (L * Math.cos(toRadians(alfa))), StartPointY + (L * Math.sin(toRadians(alfa))) + (D / Math.cos(toRadians(alfa))));////4

    linePlan(StartPointX + (L * Math.cos(toRadians(alfa))), StartPointY + (L * Math.sin(toRadians(alfa))) + (D / Math.cos(toRadians(alfa))), StartPointX + (L * Math.cos(toRadians(alfa))), StartPointY + (L * Math.sin(toRadians(alfa))) + (D / Math.cos(toRadians(alfa))) + (c1 / Math.cos(toRadians(alfa))));////6
    linePlan(StartPointX + (L * Math.cos(toRadians(alfa))), StartPointY + (c1 / Math.cos(toRadians(alfa))) + (L * Math.sin(toRadians(alfa))) + (D / Math.cos(toRadians(alfa))), StartPointX + (L * Math.cos(toRadians(alfa))), StartPointY + (L * Math.sin(toRadians(alfa))) + (D / Math.cos(toRadians(alfa))) + (c1 / Math.cos(toRadians(alfa))) + (c2 / Math.cos(toRadians(alfa))));////6

    linePlan(StartPointX + (L * Math.cos(toRadians(alfa))), StartPointY + (c1 / Math.cos(toRadians(alfa))) + (L * Math.sin(toRadians(alfa))) + (D / Math.cos(toRadians(alfa))) + (c2 / Math.cos(toRadians(alfa))), StartPointX + (L * Math.cos(toRadians(alfa))), StartPointY + (L * Math.sin(toRadians(alfa))) + (D / Math.cos(toRadians(alfa))) + (c1 / Math.cos(toRadians(alfa))) + (c2 / Math.cos(toRadians(alfa))) + 2 * D);////1
}

function DrawDastak1(h, x, alfa, D, L, w1, b2, c1, c2, j, alfaw1, hminw1, StartPointX, StartPointY, Scale, Meyar) {
    h *= Scale; x *= Scale; D *= Scale; L *= Scale; w1 *= Scale;
    b2 *= Scale; c1 *= Scale; c2 *= Scale; hminw1 *= Scale;
    fix1 = 0.35 * 100; fix1 *= Scale;

    Tool = D + ((2 * c1) - (2 * j));
    linePlan(StartPointX - (2 * j), StartPointY, StartPointX - (2 * j) - (w1 * Math.sin(toRadians(alfaw1))), StartPointY - (w1 * (-1) * Math.cos(toRadians(alfaw1))));////1

    //cosalfaw1 = Math.sin(toRadians(alfaw1));

    //y1 = (cosalfaw1 == 0 ? 0 : fix1 / (Math.sin(toRadians(alfaw1))));
    //x1 = (cosalfaw1 == 0 ? 0 : fix1 / (Math.sin(toRadians(alfaw1))));

    //y1 = (fix1 * (Math.cos(toRadians(alfaw1)) * (Math.tan(toRadians(alfaw1)))));



    //y1 = fix1 * (Math.sin(toRadians(alfaw1)));
    //x1 = fix1 * (-1) * (Math.cos(toRadians(alfaw1)));
    T = (c1 + c2 + j) / Math.cos(toRadians(alfa));
   
    y1 = 0;
    x1 = 0;
    y1End = 0;
    x1End = 0;
    Q = fix1 / Math.sin(toRadians(alfaw1))
    if (Q <= T) {
        x1 = 0; y1 = Q;
        x1End = 0; y1End = Q;
    }
    else {
        x1 = (-1) * fix1 * Math.cos(toRadians(alfaw1)) + (T - fix1 * Math.sin(toRadians(alfaw1))) * Math.tan(toRadians(alfaw1))
        y1 = T;

        x1End = (-1) * fix1 * Math.cos(toRadians(alfaw1));
        y1End = fix1 * Math.sin(toRadians(alfaw1));
    }

    y2 = 0;
    x2 = 0;
    y2End = 0;
    x2End = 0;

    Q = (fix1) / Math.sin(toRadians(alfaw1));
    if (Q <= T) {
        x2 = 0; y2 = (fix1 + x) / Math.sin(toRadians(alfaw1));
        x2End = 0;
        y2End = (fix1 + x) / Math.sin(toRadians(alfaw1));
    }
    else {
        x2 = (-1) * (fix1 + x) * Math.cos(toRadians(alfaw1)) + (T - (fix1 + x) * Math.sin(toRadians(alfaw1))) * Math.tan(toRadians(alfaw1))
        y2 = T;
        x2End = (-1) * (fix1+x) * Math.cos(toRadians(alfaw1));
        y2End = (fix1+x) * Math.sin(toRadians(alfaw1));
    }

    y3 = 0;
    x3 = 0;
    x3End = 0;
    y3End = 0;
    Q = (fix1) / Math.sin(toRadians(alfaw1))
    if (Q <= T) {
        x3 = 0; y3 = (fix1 + x + (h / 3)) / Math.sin(toRadians(alfaw1));
        x3End = 0; y3End = (fix1 + x + (hminw1 / 3)) / Math.sin(toRadians(alfaw1));
    }
    else {
        x3 = (-1) * (fix1 + x + (h / 3)) * Math.cos(toRadians(alfaw1)) + (T - (fix1 + x + (h / 3)) * Math.sin(toRadians(alfaw1))) * Math.tan(toRadians(alfaw1))
        y3 = T;
        x3End = (-1) * (fix1 + x + (hminw1 / 3)) * Math.cos(toRadians(alfaw1));
        y3End = (fix1 + x + (hminw1 / 3)) * Math.sin(toRadians(alfaw1));
    }

    linePlan(StartPointX - (2 * j) + x1, StartPointY - j - y1, StartPointX - (2 * j) - (w1 * Math.sin(toRadians(alfaw1))) + x1End, StartPointY - j - (w1 * (-1) * Math.cos(toRadians(alfaw1))) - y1End);////1
    linePlan(StartPointX - (2 * j) + (x2), StartPointY - j - y2, StartPointX - (2 * j) - (w1 * Math.sin(toRadians(alfaw1))) + (x2End), StartPointY - j - (w1 * (-1) * Math.cos(toRadians(alfaw1))) - (y2End));////1
    linePlan(StartPointX - (2 * j) + (x3), StartPointY - j - y3, StartPointX - (2 * j) - (w1 * Math.sin(toRadians(alfaw1))) + (x3End), StartPointY - j - (w1 * (-1) * Math.cos(toRadians(alfaw1))) - (y3End));////1

    if (Q <= T) {
        linePlan(StartPointX - (2 * j), StartPointY - j, StartPointX - (2 * j) + x1, StartPointY - j - y1);////1
        linePlan(StartPointX - (2 * j) + x1, StartPointY - j - y1, StartPointX - (2 * j) + x2, StartPointY - j - y2);////1
        linePlan(StartPointX - (2 * j) + x2, StartPointY - j - y1, StartPointX - (2 * j) + x3, StartPointY - j - y3);////1
    }
    else {
        fixNew1 = 0
        fixNew2 = 0
        fixNew3 = 0
        if (alfa > 0) {
            fixNew1 = (x1 - (2 * j)) * Math.sin(toRadians(alfa)) - (2 * j);
            fixNew2 = (x2 - (2 * j)) * Math.sin(toRadians(alfa)) - (2 * j);
            fixNew3 = (x3 - (2 * j)) * Math.sin(toRadians(alfa)) - (2 * j);
        }
        else {
            fixNew1 = (x1 - (2 * j)) * Math.sin(toRadians(alfa));
            fixNew2 = (x2 - (2 * j)) * Math.sin(toRadians(alfa));
            fixNew3 = (x3 - (2 * j)) * Math.sin(toRadians(alfa));
        }
        xnew1 = fixNew1 * Math.sin(toRadians(alfa));
        ynew1 = fixNew1 * Math.cos(toRadians(alfa));
        xnew2 = fixNew2 * Math.sin(toRadians(alfa));
        ynew2 = fixNew2 * Math.cos(toRadians(alfa));
        xnew3 = fixNew3 * Math.sin(toRadians(alfa));
        ynew3 = fixNew3 * Math.cos(toRadians(alfa));

        linePlan(StartPointX - (2 * j), StartPointY , StartPointX - (2 * j), StartPointY - (c1 + c2) - (2 * j));////1
        linePlan(StartPointX - (2 * j), StartPointY - (c1 + c2) - (2 * j), StartPointX - (2 * j) + (x1) - xnew1, StartPointY - j - y3 + ynew1);////1
        linePlan(StartPointX - (2 * j) + (x1) - xnew1, StartPointY - j - y3 + ynew1, StartPointX - (2 * j) + (x2) - xnew2, StartPointY - j - y2 + ynew2);////1
        linePlan(StartPointX - (2 * j) + (x2) - xnew2, StartPointY - j - y2 + ynew2, StartPointX - (2 * j) + (x3) - xnew3, StartPointY - j - y3 + ynew3);////1

        linePlan(StartPointX - (2 * j) + x1, StartPointY - j - y3, StartPointX - (2 * j) + (x1) - xnew1, StartPointY - j - y1 + ynew1);////1
        linePlan(StartPointX - (2 * j) + x2, StartPointY - j - y3, StartPointX - (2 * j) + (x2) - xnew2, StartPointY - j - y2 + ynew2);////2
        linePlan(StartPointX - (2 * j) + x3, StartPointY - j - y3, StartPointX - (2 * j) + (x3) - xnew3, StartPointY - j - y3 + ynew3);////3
    }

    //linePlan(StartPointX - (2 * j) + x1, StartPointY - y1, StartPointX - (2 * j) + (x2), StartPointY - (y2));////1
    //linePlan(StartPointX - (2 * j) + (x2), StartPointY - (y2), StartPointX - (2 * j) + (x3), StartPointY - (y3));////1

    //linePlan(StartPointX - (2 * j), StartPointY, StartPointX - (2 * j) + x1, StartPointY - y1);////1
    //linePlan(StartPointX - (2 * j) + x1, StartPointY - y1, StartPointX - (2 * j) + (x2), StartPointY - (y2));////1
    //linePlan(StartPointX - (2 * j) + (x2), StartPointY - (y2), StartPointX - (2 * j) + (x3), StartPointY - (y3));////1

    //linePlan(StartPointX - j +x1, StartPointY - y1, StartPointX - j - (w1 * Math.sin(toRadians(alfaw1))) + x1, StartPointY - (w1 * (-1) * Math.cos(toRadians(alfaw1))) + y1);////1
    //linePlan(StartPointX - j, StartPointY - (y1 + y2), StartPointX -j- (w1 * Math.cos(toRadians(alfaw1))), StartPointY - (y1 + y2) - (w1 * Math.sin(toRadians(alfaw1))));////1
    //linePlan(StartPointX - j, StartPointY - (y1 + y2 + y3), StartPointX -j- (w1 * Math.cos(toRadians(alfaw1))), StartPointY - (y1 + y2 + y3) - (w1 * Math.sin(toRadians(alfaw1))));////1


    //y1prim = (-1)*((fix1 / (Math.sin(toRadians(alfaw1)))) - (fix1 * (Math.sin(toRadians(alfaw1)))));
    //x1prim = fix1 *  (Math.cos(toRadians(alfaw1)));

    //y2 = x * (Math.sin(toRadians(alfaw1)));
    //x2 = x * (-1) * (Math.cos(toRadians(alfaw1)));

    //y3 = (h / 3) * (Math.sin(toRadians(alfaw1)));
    //yEnd3 = (hminw1 / 3) * (Math.sin(toRadians(alfaw1)));
    //x3 = (h / 3) * (-1) * (Math.cos(toRadians(alfaw1)));
    //xEnd3 = (hminw1 / 3) * (-1) * (Math.cos(toRadians(alfaw1)));

    //y2 = (cosalfaw1 == 0 ? 0 : x / Math.sin(toRadians(alfaw1)));
    //y3 = (cosalfaw1 == 0 ? 0 : (h / 3) / Math.sin(toRadians(alfaw1)));
    //y1 = y1 < 0 ? (-1 * y1) : y1;
    //y2 = y2 < 0 ? (-1 * y2) : y2;
    //y3 = y3 < 0 ? (-1 * y3) : y3;
}

function DrawDastak2(h, x, alfa, D, L, w2, b2, c1, c2, j, alfaw2, hminw2, StartPointX, StartPointY, Scale, Meyar) {
    h *= Scale; x *= Scale; D *= Scale; L *= Scale; w2 *= Scale;
    b2 *= Scale; c1 *= Scale; c2 *= Scale; hminw2 *= Scale;
    fix1 = 0.35 * 100; fix1 *= Scale;

    Tool = D + ((2 * c1) - (2 * j));
    linePlan(StartPointX - (2 * j), StartPointY + (D / Math.cos(toRadians(alfa))), StartPointX - (2 * j) - (w2 * Math.sin(toRadians(alfaw2))), StartPointY + (D / Math.cos(toRadians(alfa))) - (w2 * Math.cos(toRadians(alfaw2))));////6

    y1 = 0;
    x1 = 0;
    y1End = 0;
    x1End = 0;
    Q = fix1 / Math.sin(toRadians(alfaw2))
    T = c1 + c2 + j;
    if (Q <= T) {
        x1 = 0; y1 = Q;
        x1End = 0; y1End = Q;
    }
    else {
        x1 = (-1) * fix1 * Math.cos(toRadians(alfaw2)) + (T - fix1 * Math.sin(toRadians(alfaw2))) * Math.tan(toRadians(alfaw2))
        y1 = T;

        x1End = (-1) * fix1 * Math.cos(toRadians(alfaw2));
        y1End = fix1 * Math.sin(toRadians(alfaw2));
    }

    y2 = 0;
    x2 = 0;
    y2End = 0;
    x2End = 0;

    Q = (fix1) / Math.sin(toRadians(alfaw2));
    T = c1 + c2 + j;
    if (Q <= T) {
        x2 = 0; y2 = (fix1 + x) / Math.sin(toRadians(alfaw2));
        x2End = 0;
        y2End = (fix1 + x) / Math.sin(toRadians(alfaw2));
    }
    else {
        x2 = (-1) * (fix1 + x) * Math.cos(toRadians(alfaw2)) + (T - (fix1 + x) * Math.sin(toRadians(alfaw2))) * Math.tan(toRadians(alfaw2))
        y2 = T;
        x2End = (-1) * (fix1 + x) * Math.cos(toRadians(alfaw2));
        y2End = (fix1 + x) * Math.sin(toRadians(alfaw2));
    }

    y3 = 0;
    x3 = 0;
    x3End = 0;
    y3End = 0;
    Q = (fix1) / Math.sin(toRadians(alfaw2))
    T = c1 + c2 + j;
    if (Q <= T) {
        x3 = 0; y3 = (fix1 + x + (h / 3)) / Math.sin(toRadians(alfaw2));
        x3End = 0; y3End = (fix1 + x + (hminw2 / 3)) / Math.sin(toRadians(alfaw2));
    }
    else {
        x3 = (-1) * (fix1 + x + (h / 3)) * Math.cos(toRadians(alfaw2)) + (T - (fix1 + x + (h / 3)) * Math.sin(toRadians(alfaw2))) * Math.tan(toRadians(alfaw2))
        y3 = T;
        x3End = (-1) * (fix1 + x + (hminw2 / 3)) * Math.cos(toRadians(alfaw2));
        y3End = (fix1 + x + (hminw2 / 3)) * Math.sin(toRadians(alfaw2));
    }

    linePlan(StartPointX - (2 * j) + x1, StartPointY + j+ (D / Math.cos(toRadians(alfa)))  + y1, StartPointX - (2 * j) - (w2 * Math.sin(toRadians(alfaw2))) + x1End, StartPointY + j + (D / Math.cos(toRadians(alfa))) - (w2 * Math.cos(toRadians(alfaw2))) + y1End);////6
    linePlan(StartPointX - (2 * j) + (x2), StartPointY + j + (D / Math.cos(toRadians(alfa))) + (y2), StartPointX - (2 * j) - (w2 * Math.sin(toRadians(alfaw2))) + (x2End), StartPointY + j + (D / Math.cos(toRadians(alfa))) - (w2 * Math.cos(toRadians(alfaw2))) + (y2End));////6
    linePlan(StartPointX - (2 * j) + (x3), StartPointY + j + (D / Math.cos(toRadians(alfa))) + (y3), StartPointX - (2 * j) - (w2 * Math.sin(toRadians(alfaw2))) + (x3End), StartPointY + j + (D / Math.cos(toRadians(alfa))) - (w2 * Math.cos(toRadians(alfaw2))) + (y3End));////6

    if (Q <= T) {
        linePlan(StartPointX - (2 * j), StartPointY + (D / Math.cos(toRadians(alfa))), StartPointX - (2 * j) - x1, StartPointY + (D / Math.cos(toRadians(alfa))) + y1);////1
        linePlan(StartPointX - (2 * j) - x1, StartPointY + (D / Math.cos(toRadians(alfa))) + y1, StartPointX - (2 * j) + x2, StartPointY + (D / Math.cos(toRadians(alfa))) + y2);////1
        linePlan(StartPointX - (2 * j) + x2, StartPointY + (D / Math.cos(toRadians(alfa))) + y2, StartPointX - (2 * j) + x3, StartPointY + (D / Math.cos(toRadians(alfa))) + y3);////1
    }
    else {
        fixNew1 = 0
        fixNew2 = 0
        fixNew3 = 0
        if (alfa > 0) {
            fixNew1 = (x1 - (2 * j)) * Math.sin(toRadians(alfa)) - (2 * j);
            fixNew2 = (x2 - (2 * j)) * Math.sin(toRadians(alfa)) - (2 * j);
            fixNew3 = (x3 - (2 * j)) * Math.sin(toRadians(alfa)) - (2 * j);
        }
        else {
            fixNew1 = (x1 - (2 * j)) * Math.sin(toRadians(alfa));
            fixNew2 = (x2 - (2 * j)) * Math.sin(toRadians(alfa));
            fixNew3 = (x3 - (2 * j)) * Math.sin(toRadians(alfa));
        }
        xnew1 = fixNew1 * Math.sin(toRadians(alfa));
        ynew1 = fixNew1 * Math.cos(toRadians(alfa));
        xnew2 = fixNew2 * Math.sin(toRadians(alfa));
        ynew2 = fixNew2 * Math.cos(toRadians(alfa));
        xnew3 = fixNew3 * Math.sin(toRadians(alfa));
        ynew3 = fixNew3 * Math.cos(toRadians(alfa));

        linePlan(StartPointX - (2 * j), StartPointY + (D / Math.cos(toRadians(alfa))), StartPointX - (2 * j), StartPointY + (D / Math.cos(toRadians(alfa))) + (c1 + c2) + (2 * j));////1
        linePlan(StartPointX - (2 * j), StartPointY + (D / Math.cos(toRadians(alfa))) + (c1 + c2) + (2 * j), StartPointX - (2 * j) + x1 - xnew1, StartPointY + j + (D / Math.cos(toRadians(alfa))) + y1 - ynew1);////1
        linePlan(StartPointX - (2 * j) + x1 - xnew1, StartPointY + j + (D / Math.cos(toRadians(alfa))) + y1 - ynew1, StartPointX - (2 * j) + x2 - xnew2, StartPointY + j + (D / Math.cos(toRadians(alfa))) + y2 - ynew2);////1
        linePlan(StartPointX - (2 * j) + x2 - xnew2, StartPointY + j + (D / Math.cos(toRadians(alfa))) + y2 - ynew2, StartPointX - (2 * j) + x3 - ynew3, StartPointY + j + (D / Math.cos(toRadians(alfa))) + y3 - ynew3);////1

        //linePlan(StartPointX - (2 * j) + (x1), StartPointY - y3, StartPointX - (2 * j) + (x1) - xnew1, StartPointY - y1 + ynew1);////1
        //linePlan(StartPointX - (2 * j) + (x2), StartPointY - y3, StartPointX - (2 * j) + (x2) - xnew2, StartPointY - y2 + ynew2);////2
        //linePlan(StartPointX - (2 * j) + (x3), StartPointY - y3, StartPointX - (2 * j) + (x3) - xnew3, StartPointY - y3 + ynew3);////3
 
    }

    //linePlan(StartPointX - j, StartPointY + (D / Math.cos(toRadians(alfa))), StartPointX - j + x1, StartPointY + (D / Math.cos(toRadians(alfa))) + y1);////1
    //linePlan(StartPointX - j + x1, StartPointY + (D / Math.cos(toRadians(alfa))) + y1, StartPointX - j + (x1 + x2), StartPointY + (D / Math.cos(toRadians(alfa))) + (y1 + y2));////1
    //linePlan(StartPointX - j + (x1 + x2), StartPointY + (D / Math.cos(toRadians(alfa))) + (y1 + y2), StartPointX - j + (x1 + x2 + x3), StartPointY + (D / Math.cos(toRadians(alfa))) + (y1 + y2 + y3));////1

    //linePlan(StartPointX - j, StartPointY + (D / Math.cos(toRadians(alfa))) + y1, StartPointX - (w2 * Math.cos(toRadians(-90 + alfaw2))), StartPointY + (D / Math.cos(toRadians(alfa))) + y1 + (w2 * Math.sin(toRadians(-90 + alfaw2))));////1
    //linePlan(StartPointX - j, StartPointY + (D / Math.cos(toRadians(alfa))) + (y1 + y2), StartPointX - (w2 * Math.cos(toRadians(-90 + alfaw2))), StartPointY + (D / Math.cos(toRadians(alfa))) + (y1 + y2) + (w2 * Math.sin(toRadians(-90 + alfaw2))));////1
    //linePlan(StartPointX - j, StartPointY + (D / Math.cos(toRadians(alfa))) + (y1 + y2 + y3), StartPointX - (w2 * Math.cos(toRadians(-90 + alfaw2))), StartPointY + (D / Math.cos(toRadians(alfa))) + (y1 + y2 + y3) + (w2 * Math.sin(toRadians(-90 + alfaw2))));////1

    //cosalfaw2 = Math.sin(toRadians(-90 + alfaw2));

    //y1 = fix1 * (-1) * Math.sin(toRadians(alfaw2));
    //x1 = fix1 * (-1) * Math.cos(toRadians(alfaw2));

    //y2 = x * (-1) * Math.sin(toRadians(alfaw2));
    //x2 = x * (-1) * Math.cos(toRadians(alfaw2));

    //y3 = (h/3) * (-1) * Math.sin(toRadians(alfaw2));
    //x3 = (h/3) * (-1) * Math.cos(toRadians(alfaw2));

    //y2 = x / Math.sin(toRadians(-90 + alfaw2));
    //y3 = (h / 3) / Math.sin(toRadians(-90 + alfaw2));
    //y1 = y1 < 0 ? (-1 * y1) : y1;
    //y2 = y2 < 0 ? (-1 * y2) : y2;
    //y3 = y3 < 0 ? (-1 * y3) : y3;
}

function DrawDastak3(h, x, alfa, D, L, w3, b2, c1, c2, j, alfaw3, hminw3, StartPointX, StartPointY, Scale, Meyar) {
    h *= Scale; x *= Scale; D *= Scale; L *= Scale; w3 *= Scale;
    b2 *= Scale; c1 *= Scale; c2 *= Scale; hminw3 *= Scale;
    fix1 = 0.35 * 100; fix1 *= Scale;

    Tool = D + ((2 * c1) - (2 * j));
    linePlan(StartPointX + (2 * j) + (L * Math.cos(toRadians(alfa))), StartPointY + j + (L * Math.sin(toRadians(alfa))), StartPointX + (2 * j) + (L * Math.cos(toRadians(alfa))) - (w3 * (-1) * Math.sin(toRadians(alfaw3))), StartPointY + j + (L * Math.sin(toRadians(alfa))) - (w3 * (-1) * Math.cos(toRadians(alfaw3))));////1

    y1 = 0;
    x1 = 0;
    y1End = 0;
    x1End = 0;
    Q = fix1 / Math.sin(toRadians(alfaw3));
    T = c1 + c2 + j;
    if (Q <= T) {
        x1 = 0; y1 = Q;
        x1End = 0; y1End = Q;
    }
    else {
        x1 = (-1) * fix1 * Math.cos(toRadians(alfaw3)) + (T - fix1 * Math.sin(toRadians(alfaw3))) * Math.tan(toRadians(alfaw3))
        y1 = T;

        x1End = (-1) * fix1 * Math.cos(toRadians(alfaw3));
        y1End = fix1 * Math.sin(toRadians(alfaw3));
    }

    y2 = 0;
    x2 = 0;
    y2End = 0;
    x2End = 0;

    Q = (fix1) / Math.sin(toRadians(alfaw3));
    T = c1 + c2 + j;
    if (Q <= T) {
        x2 = 0; y2 = (fix1 + x) / Math.sin(toRadians(alfaw3));
        x2End = 0;
        y2End = (fix1 + x) / Math.sin(toRadians(alfaw3));
    }
    else {
        x2 = (-1) * (fix1 + x) * Math.cos(toRadians(alfaw3)) + (T - (fix1 + x) * Math.sin(toRadians(alfaw3))) * Math.tan(toRadians(alfaw3))
        y2 = T;
        x2End = (-1) * (fix1 + x) * Math.cos(toRadians(alfaw3));
        y2End = (fix1 + x) * Math.sin(toRadians(alfaw3));
    }

    y3 = 0;
    x3 = 0;
    x3End = 0;
    y3End = 0;
    Q = (fix1) / Math.sin(toRadians(alfaw3))
    T = c1 + c2 + j;
    if (Q <= T) {
        x3 = 0; y3 = (fix1 + x + (h / 3)) / Math.sin(toRadians(alfaw3));
        x3End = 0; y3End = (fix1 + x + (hminw3 / 3)) / Math.sin(toRadians(alfaw3));
    }
    else {
        x3 = (-1) * (fix1 + x + (h / 3)) * Math.cos(toRadians(alfaw3)) + (T - (fix1 + x + (h / 3)) * Math.sin(toRadians(alfaw3))) * Math.tan(toRadians(alfaw3))
        y3 = T;
        x3End = (-1) * (fix1 + x + (hminw3 / 3)) * Math.cos(toRadians(alfaw3));
        y3End = (fix1 + x + (hminw3 / 3)) * Math.sin(toRadians(alfaw3));
    }

    linePlan(StartPointX + (2 * j) + (L * Math.cos(toRadians(alfa))) - x1, StartPointY - j + (L * Math.sin(toRadians(alfa))) - y1, StartPointX + (2 * j) + (L * Math.cos(toRadians(alfa))) - (w3 * (-1) * Math.sin(toRadians(alfaw3))) - x1End, StartPointY - j + (L * Math.sin(toRadians(alfa))) - (w3 * (-1) * Math.cos(toRadians(alfaw3))) - y1End);////1
    linePlan(StartPointX + (2 * j) + (L * Math.cos(toRadians(alfa))) - x2, StartPointY - j + (L * Math.sin(toRadians(alfa))) - (y2), StartPointX + (2 * j) + (L * Math.cos(toRadians(alfa))) - (w3 * (-1) * Math.sin(toRadians(alfaw3))) - x2End, StartPointY -j+ (L * Math.sin(toRadians(alfa))) - (w3 * (-1) * Math.cos(toRadians(alfaw3))) - y2End);////1
    linePlan(StartPointX + (2 * j) + (L * Math.cos(toRadians(alfa))) - x3, StartPointY - j + (L * Math.sin(toRadians(alfa))) - (y3), StartPointX + (2 * j) + (L * Math.cos(toRadians(alfa))) - (w3 * (-1) * Math.sin(toRadians(alfaw3))) - x3End, StartPointY -j+ (L * Math.sin(toRadians(alfa))) - (w3 * (-1) * Math.cos(toRadians(alfaw3))) - y3End);////1



    if (Q <= T) {
        linePlan(StartPointX + (2 * j) + (L * Math.cos(toRadians(alfa))), StartPointY + j + (L * Math.sin(toRadians(alfa))), StartPointX + (2 * j) + (L * Math.cos(toRadians(alfa))) - x1, StartPointY + j + (L * Math.sin(toRadians(alfa))) - y1);////1
        linePlan(StartPointX + (2 * j) + (L * Math.cos(toRadians(alfa))) - x1, StartPointY + j + (L * Math.sin(toRadians(alfa))) - y1, StartPointX + (2 * j) + (L * Math.cos(toRadians(alfa))) - x2, StartPointY + j + (L * Math.sin(toRadians(alfa))) - y2);////1
        linePlan(StartPointX + (2 * j) + (L * Math.cos(toRadians(alfa))) - x2, StartPointY + j + (L * Math.sin(toRadians(alfa))) - y2, StartPointX + (2 * j) + (L * Math.cos(toRadians(alfa))) - x3, StartPointY + j + (L * Math.sin(toRadians(alfa))) - y3);////1
    }
    else {
        fixNew1 = 0
        fixNew2 = 0
        fixNew3 = 0
        if (alfa > 0) {
            fixNew1 = (x1 - (2 * j)) * Math.sin(toRadians(alfa)) - (2 * j);
            fixNew2 = (x2 - (2 * j)) * Math.sin(toRadians(alfa)) - (2 * j);
            fixNew3 = (x3 - (2 * j)) * Math.sin(toRadians(alfa)) - (2 * j);
        }
        else {
            fixNew1 = (x1 - (2 * j)) * Math.sin(toRadians(alfa));
            fixNew2 = (x2 - (2 * j)) * Math.sin(toRadians(alfa));
            fixNew3 = (x3 - (2 * j)) * Math.sin(toRadians(alfa));
        }
        xnew1 = fixNew1 * Math.sin(toRadians(alfa));
        ynew1 = fixNew1 * Math.cos(toRadians(alfa));
        xnew2 = fixNew2 * Math.sin(toRadians(alfa));
        ynew2 = fixNew2 * Math.cos(toRadians(alfa));
        xnew3 = fixNew3 * Math.sin(toRadians(alfa));
        ynew3 = fixNew3 * Math.cos(toRadians(alfa));


        linePlan(StartPointX + (2 * j) + (L * Math.cos(toRadians(alfa))), StartPointY + j + (L * Math.sin(toRadians(alfa))), StartPointX + (2 * j) + (L * Math.cos(toRadians(alfa))), StartPointY + (L * Math.sin(toRadians(alfa))) - (c1 + c2) - (2 * j));////1
        linePlan(StartPointX + (2 * j) + (L * Math.cos(toRadians(alfa))), StartPointY + (L * Math.sin(toRadians(alfa))) - (c1 + c2) - (2 * j), StartPointX + (2 * j) + (L * Math.cos(toRadians(alfa))) - x1+xnew1, StartPointY - j + (L * Math.sin(toRadians(alfa))) - y1+ynew1);////1
        linePlan(StartPointX + (2 * j) + (L * Math.cos(toRadians(alfa))) - x1 + xnew1, StartPointY - j + (L * Math.sin(toRadians(alfa))) - y1 + ynew1, StartPointX + (2 * j) + (L * Math.cos(toRadians(alfa))) - x2 + xnew2, StartPointY - j + (L * Math.sin(toRadians(alfa))) - y2 + ynew2);////1
        linePlan(StartPointX + (2 * j) + (L * Math.cos(toRadians(alfa))) - x2 + xnew2, StartPointY - j + (L * Math.sin(toRadians(alfa))) - y2 + ynew2, StartPointX + (2 * j) + (L * Math.cos(toRadians(alfa))) - x3 + xnew3, StartPointY - j + (L * Math.sin(toRadians(alfa))) - y3 + ynew3);////1

        linePlan(StartPointX - (2 * j) + x1, StartPointY - j - y3, StartPointX - (2 * j) + (x1) - xnew1, StartPointY - j - y1 + ynew1);////1
        linePlan(StartPointX - (2 * j) + x2, StartPointY - j - y3, StartPointX - (2 * j) + (x2) - xnew2, StartPointY - j - y2 + ynew2);////2
        linePlan(StartPointX - (2 * j) + x3, StartPointY - j - y3, StartPointX - (2 * j) + (x3) - xnew3, StartPointY - j - y3 + ynew3);////3
    }
    //y1 = fix1 * Math.sin(toRadians(alfaw3));
    //x1 = fix1 * Math.cos(toRadians(alfaw3));

    //y2 = x * Math.sin(toRadians(alfaw3));
    //x2 = x * Math.cos(toRadians(alfaw3));

    //y3 = (h/3) * Math.sin(toRadians(alfaw3));
    //x3 = (h/3) * Math.cos(toRadians(alfaw3));
    //cosalfaw3 = Math.sin(toRadians(-90 + alfaw3));

    //y1 = (cosalfaw3 == 0 ? 0 : fix1 / Math.sin(toRadians(-90 + alfaw3)));
    //y2 = (cosalfaw3 == 0 ? 0 : x / Math.sin(toRadians(-90 + alfaw3)));
    //y3 = (cosalfaw3 == 0 ? 0 : (h / 3) / Math.sin(toRadians(-90 + alfaw3)));
    //y1 = y1 < 0 ? (-1 * y1) : y1;
    //y2 = y2 < 0 ? (-1 * y2) : y2;
    //y3 = y3 < 0 ? (-1 * y3) : y3;

    //linePlan(StartPointX + j + (L * Math.cos(toRadians(alfa))), StartPointY + (L * Math.sin(toRadians(alfa))) - y1, StartPointX + (L * Math.cos(toRadians(alfa))) + (w3 * Math.cos(toRadians(-90 + alfaw3))), StartPointY + (L * Math.sin(toRadians(alfa))) - (w3 * Math.sin(toRadians(-90 + alfaw3))) - y1);////1
    //linePlan(StartPointX + j + (L * Math.cos(toRadians(alfa))), StartPointY + (L * Math.sin(toRadians(alfa))) - (y1 + y2), StartPointX + (L * Math.cos(toRadians(alfa))) + (w3 * Math.cos(toRadians(-90 + alfaw3))), StartPointY + (L * Math.sin(toRadians(alfa))) - (w3 * Math.sin(toRadians(-90 + alfaw3))) - (y1 + y2));////1
    //linePlan(StartPointX + j + (L * Math.cos(toRadians(alfa))), StartPointY + (L * Math.sin(toRadians(alfa))) - (y1 + y2 + y3), StartPointX + (L * Math.cos(toRadians(alfa))) + (w3 * Math.cos(toRadians(-90 + alfaw3))), StartPointY + (L * Math.sin(toRadians(alfa))) - (w3 * Math.sin(toRadians(-90 + alfaw3))) - (y1 + y2 + y3));////1
}

function DrawDastak4(h, x, alfa, D, L, w4, b2, c1, c2, j, alfaw4, hminw4, StartPointX, StartPointY, Scale, Meyar) {
    h *= Scale; x *= Scale; D *= Scale; L *= Scale; w4 *= Scale;
    b2 *= Scale; c1 *= Scale; c2 *= Scale; hminw4 *= Scale;
    fix1 = 0.35 * 100; fix1 *= Scale;

    Tool = D + ((2 * c1) - (2 * j));
    linePlan(StartPointX + (2 * j) + (L * Math.cos(toRadians(alfa))), StartPointY + (L * Math.sin(toRadians(alfa))) + (D / Math.cos(toRadians(alfa))), StartPointX + (2 * j) + (L * Math.cos(toRadians(alfa))) + (w4 * Math.sin(toRadians(alfaw4))), StartPointY + (L * Math.sin(toRadians(alfa))) + (D / Math.cos(toRadians(alfa))) - (w4 * Math.cos(toRadians(alfaw4))));////6

    y1 = 0;
    x1 = 0;
    y1End = 0;
    x1End = 0;
    Q = fix1 / Math.sin(toRadians(alfaw4));
    T = c1 + c2 + j;
    if (Q <= T) {
        x1 = 0; y1 = Q;
        x1End = 0; y1End = Q;
    }
    else {
        x1 = (-1) * fix1 * Math.cos(toRadians(alfaw4)) + (T - fix1 * Math.sin(toRadians(alfaw4))) * Math.tan(toRadians(alfaw4))
        y1 = T;

        x1End = (-1) * fix1 * Math.cos(toRadians(alfaw4));
        y1End = fix1 * Math.sin(toRadians(alfaw4));
    }

    y2 = 0;
    x2 = 0;
    y2End = 0;
    x2End = 0;

    Q = (fix1) / Math.sin(toRadians(alfaw4));
    T = c1 + c2 + j;
    if (Q <= T) {
        x2 = 0; y2 = (fix1 + x) / Math.sin(toRadians(alfaw4));
        x2End = 0;
        y2End = (fix1 + x) / Math.sin(toRadians(alfaw4));
    }
    else {
        x2 = (-1) * (fix1 + x) * Math.cos(toRadians(alfaw4)) + (T - (fix1 + x) * Math.sin(toRadians(alfaw4))) * Math.tan(toRadians(alfaw4))
        y2 = T;
        x2End = (-1) * (fix1 + x) * Math.cos(toRadians(alfaw4));
        y2End = (fix1 + x) * Math.sin(toRadians(alfaw4));
    }

    y3 = 0;
    x3 = 0;
    x3End = 0;
    y3End = 0;
    Q = (fix1) / Math.sin(toRadians(alfaw4))
    T = c1 + c2 + j;
    if (Q <= T) {
        x3 = 0; y3 = (fix1 + x + (h / 3)) / Math.sin(toRadians(alfaw4));
        x3End = 0; y3End = (fix1 + x + (hminw4 / 3)) / Math.sin(toRadians(alfaw4));
    }
    else {
        x3 = (-1) * (fix1 + x + (h / 3)) * Math.cos(toRadians(alfaw4)) + (T - (fix1 + x + (h / 3)) * Math.sin(toRadians(alfaw4))) * Math.tan(toRadians(alfaw4))
        y3 = T;
        x3End = (-1) * (fix1 + x + (hminw4 / 3)) * Math.cos(toRadians(alfaw4));
        y3End = (fix1 + x + (hminw4 / 3)) * Math.sin(toRadians(alfaw4));
    }

    linePlan(StartPointX + (2 * j) + (L * Math.cos(toRadians(alfa))) - x1, StartPointY + j + (L * Math.sin(toRadians(alfa))) + (D / Math.cos(toRadians(alfa))) + y1, StartPointX + (2 * j) + (L * Math.cos(toRadians(alfa))) + (w4 * Math.sin(toRadians(alfaw4))) - x1End, StartPointY + j + (L * Math.sin(toRadians(alfa))) + (D / Math.cos(toRadians(alfa))) - (w4 * Math.cos(toRadians(alfaw4))) + y1End);////6
    linePlan(StartPointX + (2 * j) + (L * Math.cos(toRadians(alfa))) - x2, StartPointY + j + (L * Math.sin(toRadians(alfa))) + (D / Math.cos(toRadians(alfa))) + y2, StartPointX + (2 * j) + (L * Math.cos(toRadians(alfa))) + (w4 * Math.sin(toRadians(alfaw4))) - x2End, StartPointY + j + (L * Math.sin(toRadians(alfa))) + (D / Math.cos(toRadians(alfa))) - (w4 * Math.cos(toRadians(alfaw4))) + y2End);////6
    linePlan(StartPointX + (2 * j) + (L * Math.cos(toRadians(alfa))) - x3, StartPointY + j + (L * Math.sin(toRadians(alfa))) + (D / Math.cos(toRadians(alfa))) + y3, StartPointX + (2 * j) + (L * Math.cos(toRadians(alfa))) + (w4 * Math.sin(toRadians(alfaw4))) - x3End, StartPointY + j + (L * Math.sin(toRadians(alfa))) + (D / Math.cos(toRadians(alfa))) - (w4 * Math.cos(toRadians(alfaw4))) + y3End);////6

    if (Q <= T) {
        linePlan(StartPointX + (2 * j) + (L * Math.cos(toRadians(alfa))), StartPointY + j + (L * Math.sin(toRadians(alfa))) + (D / Math.cos(toRadians(alfa))), StartPointX + (2 * j) + (L * Math.cos(toRadians(alfa))) - x1, StartPointY + j + (L * Math.sin(toRadians(alfa))) + (D / Math.cos(toRadians(alfa))) + y1);////1
        linePlan(StartPointX + (2 * j) + (L * Math.cos(toRadians(alfa))) - x1, StartPointY + j + (L * Math.sin(toRadians(alfa))) + (D / Math.cos(toRadians(alfa))) + y1, StartPointX + (2 * j) + (L * Math.cos(toRadians(alfa))) - x2, StartPointY + j + (L * Math.sin(toRadians(alfa))) + (D / Math.cos(toRadians(alfa))) + y2);////1
        linePlan(StartPointX + (2 * j) + (L * Math.cos(toRadians(alfa))) - x2, StartPointY + j + (L * Math.sin(toRadians(alfa))) + (D / Math.cos(toRadians(alfa))) + y2, StartPointX + (2 * j) + (L * Math.cos(toRadians(alfa))) - x3, StartPointY + j + (L * Math.sin(toRadians(alfa))) + (D / Math.cos(toRadians(alfa))) + y3);////1
    }
    else {
        fixNew1 = 0
        fixNew2 = 0
        fixNew3 = 0
        if (alfa > 0) {
            fixNew1 = (x1 - (2 * j)) * Math.sin(toRadians(alfa)) - (2 * j);
            fixNew2 = (x2 - (2 * j)) * Math.sin(toRadians(alfa)) - (2 * j);
            fixNew3 = (x3 - (2 * j)) * Math.sin(toRadians(alfa)) - (2 * j);
        }
        else {
            fixNew1 = (x1 - (2 * j)) * Math.sin(toRadians(alfa));
            fixNew2 = (x2 - (2 * j)) * Math.sin(toRadians(alfa));
            fixNew3 = (x3 - (2 * j)) * Math.sin(toRadians(alfa));
        }
        xnew1 = fixNew1 * Math.sin(toRadians(alfa));
        ynew1 = fixNew1 * Math.cos(toRadians(alfa));
        xnew2 = fixNew2 * Math.sin(toRadians(alfa));
        ynew2 = fixNew2 * Math.cos(toRadians(alfa));
        xnew3 = fixNew3 * Math.sin(toRadians(alfa));
        ynew3 = fixNew3 * Math.cos(toRadians(alfa));


        linePlan(StartPointX + (2 * j) + (L * Math.cos(toRadians(alfa))), StartPointY + j + (L * Math.sin(toRadians(alfa))) + (D / Math.cos(toRadians(alfa))), StartPointX + (2 * j) + (L * Math.cos(toRadians(alfa))), StartPointY + j + (L * Math.sin(toRadians(alfa))) + (D / Math.cos(toRadians(alfa))) + (c1 + c2) + (2 * j));////1
        linePlan(StartPointX + (2 * j) + (L * Math.cos(toRadians(alfa))), StartPointY + j + (L * Math.sin(toRadians(alfa))) + (D / Math.cos(toRadians(alfa))) + (c1 + c2) + (2 * j), StartPointX + (2 * j) + (L * Math.cos(toRadians(alfa))) - x1 - xnew1, StartPointY + j + (L * Math.sin(toRadians(alfa))) + (D / Math.cos(toRadians(alfa))) + y1 + ynew1);////1
        linePlan(StartPointX + (2 * j) + (L * Math.cos(toRadians(alfa))) - x1 - xnew1, StartPointY + j + (L * Math.sin(toRadians(alfa))) + (D / Math.cos(toRadians(alfa))) + y1 + ynew1, StartPointX + (2 * j) + (L * Math.cos(toRadians(alfa))) - x2-xnew2, StartPointY + j + (L * Math.sin(toRadians(alfa))) + (D / Math.cos(toRadians(alfa))) + y2+ynew2);////1
        linePlan(StartPointX + (2 * j) + (L * Math.cos(toRadians(alfa))) - x2 - xnew2, StartPointY + j + (L * Math.sin(toRadians(alfa))) + (D / Math.cos(toRadians(alfa))) + y2 + ynew2, StartPointX + (2 * j) + (L * Math.cos(toRadians(alfa))) - x3-xnew3, StartPointY + j + (L * Math.sin(toRadians(alfa))) + (D / Math.cos(toRadians(alfa))) + y3+ynew3);////1

        linePlan(StartPointX - (2 * j) + x1, StartPointY - j - y3, StartPointX - (2 * j) + (x1) - xnew1, StartPointY - j - y1 + ynew1);////1
        linePlan(StartPointX - (2 * j) + x2, StartPointY - j - y3, StartPointX - (2 * j) + (x2) - xnew2, StartPointY - j - y2 + ynew2);////2
        linePlan(StartPointX - (2 * j) + x3, StartPointY - j - y3, StartPointX - (2 * j) + (x3) - xnew3, StartPointY - j - y3 + ynew3);////3
    }
    //y1 = fix1 *(-1) * Math.sin(toRadians(alfaw4));
    //x1 = fix1 * Math.cos(toRadians(alfaw4));

    //y2 = x * (-1) * Math.sin(toRadians(alfaw4));
    //x2 = x * Math.cos(toRadians(alfaw4));

    //y3 = (h/3) * (-1) * Math.sin(toRadians(alfaw4));
    //x3 = (h / 3) * Math.cos(toRadians(alfaw4));

    //cosalfaw4 = Math.sin(toRadians(-90 + alfaw4));

    //y1 = (cosalfaw4 == 0 ? 0 : fix1 / Math.sin(toRadians(-90 + alfaw4)));
    //y2 = (cosalfaw4 == 0 ? 0 : x / Math.sin(toRadians(-90 + alfaw4)));
    //y3 = (cosalfaw4 == 0 ? 0 : (h / 3) / Math.sin(toRadians(-90 + alfaw4)));
    //y1 = y1 < 0 ? (-1 * y1) : y1;
    //y2 = y2 < 0 ? (-1 * y2) : y2;
    //y3 = y3 < 0 ? (-1 * y3) : y3;

    //linePlan(StartPointX + j + (L * Math.cos(toRadians(alfa))), StartPointY + (L * Math.sin(toRadians(alfa))) + (D / Math.cos(toRadians(alfa))) + y1, StartPointX + (L * Math.cos(toRadians(alfa))) + (w4 * Math.cos(toRadians(-90 + alfaw4))), StartPointY + (L * Math.sin(toRadians(alfa))) + (D / Math.cos(toRadians(alfa))) + (w4 * Math.sin(toRadians(-90 + alfaw4))) + y1);////1
    //linePlan(StartPointX + j + (L * Math.cos(toRadians(alfa))), StartPointY + (L * Math.sin(toRadians(alfa))) + (D / Math.cos(toRadians(alfa))) + (y1 + y2), StartPointX + (L * Math.cos(toRadians(alfa))) + (w4 * Math.cos(toRadians(-90 + alfaw4))), StartPointY + (L * Math.sin(toRadians(alfa))) + (D / Math.cos(toRadians(alfa))) + (w4 * Math.sin(toRadians(-90 + alfaw4))) + (y1 + y2));////1
    //linePlan(StartPointX + j + (L * Math.cos(toRadians(alfa))), StartPointY + (L * Math.sin(toRadians(alfa))) + (D / Math.cos(toRadians(alfa))) + (y1 + y2 + y3), StartPointX + (L * Math.cos(toRadians(alfa))) + (w4 * Math.cos(toRadians(-90 + alfaw4))), StartPointY + (L * Math.sin(toRadians(alfa))) + (D / Math.cos(toRadians(alfa))) + (w4 * Math.sin(toRadians(-90 + alfaw4))) + (y1 + y2 + y3));////1
}

function toRadians(angle) {
    return angle * (Math.PI / 180);
}

function lineDashedPlan(x1, y1, x2, y2) {
    debugger
    Line = "<line x1=\"" + x1 + "\" x2=\"" + x2 + "\" y1=\"" + y1 + "\" y2=\"" + y2 + "\" style=\"stroke-dasharray: 7px 5px;stroke: rgb(0,0,0); stroke-width:1\" />";
    $('#AbnieShow').find('#svgLinesPlan').append(Line);
    $('#AbnieShow').find('#svgContainerPlan').html($('#AbnieShow').find('#svgContainerPlan').html());
}

function linePlan(x1, y1, x2, y2) {
    debugger

    Line = "<line x1=\"" + x1 + "\" x2=\"" + x2 + "\" y1=\"" + y1 + "\" y2=\"" + y2 + "\" style=\"stroke: rgb(0,0,0); stroke-width:1\" />";
    $('#AbnieShow').find('#svgLinesPlan').append(Line);
    $('#AbnieShow').find('#svgContainerPlan').html($('#AbnieShow').find('#svgContainerPlan').html());
}

function DrawPlan1(alfa, D, L, w1, w2, w3, w4, b2, c1, c2, j, alfaw1, alfaw2, alfaw3, alfaw4,
   StartPointX, StartPointY, Scale, Meyar) {

    D *= Scale; L *= Scale; w1 *= Scale; w2 *= Scale; w3 *= Scale; w4 *= Scale;
    b2 *= Scale; c1 *= Scale; c2 *= Scale;
    Tool = D + ((2 * c1) - (2 * j));

    LineWithAlfaAndLength(alfa, StartPointX, StartPointY - (D / 2), L / 2);
    XEndLine1 = (L / 2) * Math.cos(toRadians(alfa));
    YEndLine1 = (L / 2) * Math.sin(toRadians(alfa));
    LineWithAlfaAndLength(alfa, StartPointX, StartPointY + (D / 2), L / 2);
    LineWithAlfaAndLength(alfa - 180, StartPointX, StartPointY - (D / 2), L / 2);
    LineWithAlfaAndLength(alfa - 180, StartPointX, StartPointY + (D / 2), L / 2);/////خط پایین اولی سمت چپ

    LineWithAlfaAndLength(90, StartPointX + XEndLine1, StartPointY + YEndLine1 - (D / 2), D)
    LineWithAlfaAndLength(90, StartPointX - XEndLine1, StartPointY - YEndLine1 - (D / 2), D)


    LineWithAlfaAndLength(alfa - 180, StartPointX + XEndLine1, StartPointY + YEndLine1 - (D / 2) - (c1 / Math.cos(toRadians(alfa))), L)
    LineWithAlfaAndLength(alfa - 180, StartPointX + XEndLine1, StartPointY + YEndLine1 - (D / 2) - ((c1 + c2) / Math.cos(toRadians(alfa))), L)
    /

    LineWithAlfaAndLength(90, StartPointX + XEndLine1, StartPointY + YEndLine1 - (D / 2) - (c1 / Math.cos(toRadians(alfa))), c1 / Math.cos(toRadians(alfa)))
    LineWithAlfaAndLength(90, StartPointX + XEndLine1, StartPointY + YEndLine1 - (D / 2) - ((c1 + c2) / Math.cos(toRadians(alfa))), (c2) / Math.cos(toRadians(alfa)))

    LineWithAlfaAndLength(90, StartPointX - XEndLine1, StartPointY - YEndLine1 - (D / 2) - (c1 / Math.cos(toRadians(alfa))), c1 / Math.cos(toRadians(alfa)))
    LineWithAlfaAndLength(90, StartPointX - XEndLine1, StartPointY - YEndLine1 - (D / 2) - ((c1 + c2) / Math.cos(toRadians(alfa))), (c2) / Math.cos(toRadians(alfa)))

    LineWithAlfaAndLength(alfa, StartPointX, StartPointY + (D / 2) + (c1 / Math.cos(toRadians(alfa))), L / 2);
    LineWithAlfaAndLength(alfa - 180, StartPointX, StartPointY + (D / 2) + (c1 / Math.cos(toRadians(alfa))), L / 2);

    LineWithAlfaAndLength(alfa, StartPointX, StartPointY + (D / 2) + ((c1 + c2) / Math.cos(toRadians(alfa))), L / 2);
    LineWithAlfaAndLength(alfa - 180, StartPointX, StartPointY + (D / 2) + ((c1 + c2) / Math.cos(toRadians(alfa))), L / 2);


    LineWithAlfaAndLength(90, StartPointX + (L / 2) * Math.cos(toRadians(alfa)), StartPointY + (D / 2) + (L / 2) * Math.sin(toRadians(alfa)), c1 / Math.cos(toRadians(alfa)));
    LineWithAlfaAndLength(90, StartPointX + (L / 2) * Math.cos(toRadians(alfa)), StartPointY + (D / 2) + (c1 / Math.cos(toRadians(alfa))) + (L / 2) * Math.sin(toRadians(alfa)), c2 / Math.cos(toRadians(alfa)));

    LineWithAlfaAndLength(90, StartPointX - (L / 2) * Math.cos(toRadians(alfa)), StartPointY + (D / 2) - (L / 2) * Math.sin(toRadians(alfa)), c1 / Math.cos(toRadians(alfa)));
    LineWithAlfaAndLength(90, StartPointX - (L / 2) * Math.cos(toRadians(alfa)), StartPointY + (D / 2) + (c1 / Math.cos(toRadians(alfa))) - (L / 2) * Math.sin(toRadians(alfa)), c2 / Math.cos(toRadians(alfa)));
}

function DrawDastakNew1(h, x, alfa, D, L, w1, b2, c1, c2, j, alfaw1, hminw1, t, StartPointX, StartPointY, Scale, Meyar) {
    h *= Scale; x *= Scale; D *= Scale; L *= Scale; w1 *= Scale;
    b2 *= Scale; c1 *= Scale; c2 *= Scale; hminw1 *= Scale; t *= Scale;
    fix1 = 0.35 * 100; fix1 *= Scale;
    fix2 = 0.25 * 100; fix2 *= Scale;
    XEndLine1 = (L / 2) * Math.cos(toRadians(alfa));
    YEndLine1 = (L / 2) * Math.sin(toRadians(alfa));
    LineWithAlfaAndLength(90 + alfaw1, StartPointX - XEndLine1, StartPointY - YEndLine1 - (D / 2), w1);

    T = (c1 + c2 + j) / Math.cos(toRadians(alfa));
    Q = fix1 / Math.sin(toRadians(alfaw1));
    if (Q <= T) {
        x1 = 0; y1 = Q;
        LineWithAlfaAndLength(-90, StartPointX - XEndLine1 - (w1 * Math.sin(toRadians(alfaw1))), StartPointY - YEndLine1 - (D / 2) + (w1 * Math.cos(toRadians(alfaw1))), Q);
        LineWithAlfaAndLength(90 + alfaw1, StartPointX - XEndLine1 - x1, StartPointY - YEndLine1 - (D / 2) - y1, w1);
        LineWithAlfaAndLength(-90, StartPointX - XEndLine1, StartPointY - YEndLine1 - (D / 2), Q);

        xX = StartPointX - XEndLine1 - (w1 * Math.sin(toRadians(alfaw1)));
        yX = StartPointY - YEndLine1 - (D / 2) + (w1 * Math.cos(toRadians(alfaw1)))-Q;
        LineWithAlfaAndLength(-90, xX, yX, x / Math.sin(toRadians(alfaw1)));

        LineWithAlfaAndLength(-90 + alfaw1, xX, yX-(x / Math.sin(toRadians(alfaw1))), w1);
        /////خط دوم کوچک پایین
        LineWithAlfaAndLength(-90, StartPointX - XEndLine1 - x1, StartPointY - YEndLine1 - (D / 2) - y1, x / Math.sin(toRadians(alfaw1)));

        ///////////خط کوچک بالا hmin
        xXhMin = xX;
        yXhMin = yX - (x / Math.sin(toRadians(alfaw1)));
        LineWithAlfaAndLength(-90, xXhMin, yXhMin, (hminw1 / 3) / Math.sin(toRadians(alfaw1)));

        //////////
        ///////////
        ///////////خط چهارم
        xhMinEnd = xX;
        //yhMinEnd = yX - (x / Math.sin(toRadians(alfaw1))) - ((hminw1 / 3) / Math.sin(toRadians(alfaw1)));
        yhMinEnd = yXhMin - ((hminw1 / 3) / Math.sin(toRadians(alfaw1)));

        xAval = ((StartPointX - XEndLine1) + ((fix1 + x + (h / 3)) / Math.sin(toRadians(alfaw1)))) - xX;
        xDovom=((StartPointX - XEndLine1) - xX);
        ZaribHSevomNew = xDovom/xAval;
        /////خط سوم کوچک پایین
        //LineWithAlfaAndLength(-90, StartPointX - XEndLine1, StartPointY - YEndLine1 - (D / 2) - y1 - (x / Math.sin(toRadians(alfaw1))), (ZaribHSevomNew * (h/3)) / Math.sin(toRadians(alfaw1)));

        //LineWithAlfaAndLength(alfaw1-180, (StartPointX - XEndLine1),
        //(StartPointY - YEndLine1 - (D / 2)), (fix1 + x + (h / 3)));

        //LineWithAlfaAndLength(45, (StartPointX - XEndLine1) + (fix1 + x + (h / 3)) * Math.cos(toRadians(alfaw1-180)),
        //    (StartPointY - YEndLine1 - (D / 2)) + (fix1 + x + (h / 3)) * Math.sin(toRadians(alfaw1 -180)),50);

        //(StartPointX - XEndLine1) + (fix1 + x + (h / 3)) / Math.sin(toRadians(alfaw1));

        //(StartPointY - YEndLine1 - (D / 2)) + (fix1 + x + (h / 3)) / Math.cos(toRadians(alfaw1));
        //alert(StartPointX - XEndLine1 - x1);
        //alert(StartPointX - XEndLine1 - x1 + ((h / 3) * Math.sin(toRadians(alfaw1))));


        xhMinEnd2 = StartPointX - XEndLine1 - x1;
        yhMinEnd2 = StartPointY - YEndLine1 - (D / 2) - y1 - (x / Math.sin(toRadians(alfaw1))) - ((ZaribHSevomNew * (h)) / Math.sin(toRadians(alfaw1)));

       // LineWithAlfaAndLength(-90 + alfaw1, xhMinEnd, yhMinEnd, w1);

        //DrawLineWithTwoPoint((StartPointX - XEndLine1) + (fix1 + x + (h / 3)) * Math.cos(toRadians(alfaw1 - 180))
        //    , (StartPointY - YEndLine1 - (D / 2)) + (fix1 + x + (h / 3)) * Math.sin(toRadians(alfaw1 - 180)), xhMinEnd, yhMinEnd);


        //LineWithAlfaAndLength(45, StartPointX - XEndLine1, (StartPointY - YEndLine1 - (D / 2)) + (fix1 + x + (h / 3)) / Math.cos(toRadians(270-alfaw1)), 80);

        LineWithAlfaAndLength(90, StartPointX - XEndLine1 - x1, (StartPointY - YEndLine1 - (D / 2)) + (fix1 + x) / Math.cos(toRadians(270 - alfaw1)), ((h / 3)) / Math.cos(toRadians(270 - alfaw1)));

        DrawLineWithTwoPoint(StartPointX - XEndLine1
        , (StartPointY - YEndLine1 - (D / 2)) + (fix1 + x + (h / 3)) / Math.cos(toRadians(270 - alfaw1)), xhMinEnd, yhMinEnd);

        //DrawLineWithTwoPoint(xhMinEnd, yhMinEnd, xhMinEnd2, yhMinEnd2);
    }
    else {
        x1 = (-1) * fix1 * Math.cos(toRadians(alfaw1)) + (T - fix1 * Math.sin(toRadians(alfaw1))) * Math.tan(toRadians(alfaw1))
        y1 = T;
        //LineWithAlfaAndLength(-90, StartPointX - XEndLine1, StartPointY - YEndLine1 - (D / 2), Q);
        //LineWithAlfaAndLength(alfaw1 - 90, StartPointX - XEndLine1 - (w1 * Math.sin(toRadians(alfaw1))) - (fix1 * Math.cos(toRadians(alfaw1))), StartPointY - YEndLine1 - (D / 2) + (w1 * Math.cos(toRadians(alfaw1))) - (fix1 * Math.sin(toRadians(alfaw1))), w1 - (T * Math.sin(toRadians(alfaw1))));
        //LineWithAlfaAndLength(180 + alfaw1, StartPointX - XEndLine1 - (w1 * Math.sin(toRadians(alfaw1))), StartPointY - YEndLine1 - (D / 2) + (w1 * Math.cos(toRadians(alfaw1))), fix1+x);

        LineWithAlfaAndLength(180 + alfaw1, StartPointX - XEndLine1 - (w1 * Math.sin(toRadians(alfaw1))), StartPointY - YEndLine1 - (D / 2) + (w1 * Math.cos(toRadians(alfaw1))), fix1);

        x1Prim = StartPointX - XEndLine1 - (w1 * Math.sin(toRadians(alfaw1))) - (fix1 * Math.cos(toRadians(alfaw1)));
        y1Prim = StartPointY - YEndLine1 - (D / 2) + (w1 * Math.cos(toRadians(alfaw1))) - (fix1 * Math.sin(toRadians(alfaw1)))

        x2Prim = StartPointX + XEndLine1;
        y2Prim = StartPointY + YEndLine1 - (D / 2) - ((c1 + c2) / Math.cos(toRadians(alfa)));

        //LineWithAlfaAndLength(50, x2Prim, y2Prim, 50);

        //xx= x1Prim + 40 * Math.sin(toRadians(alfaw1));
        //yy = y1Prim - 40 * Math.cos(toRadians(alfaw1));

        //LineWithAlfaAndLength(0, xx, yy, 50);

        //xxx = x2Prim - 200 * Math.cos(toRadians(alfa));
        //yyy = y2Prim - 200 * Math.sin(toRadians(alfa));

        //LineWithAlfaAndLength(0, xxx, yyy, 50);

        Lprim = (y1Prim - y2Prim + (x2Prim - x1Prim) * Math.tan(toRadians(alfa))) / (Math.cos(toRadians(alfaw1)) + Math.sin(toRadians(alfaw1)) * Math.tan(toRadians(alfa)));
        LineWithAlfaAndLength(alfaw1 - 90, StartPointX - XEndLine1 - (w1 * Math.sin(toRadians(alfaw1))) - (fix1 * Math.cos(toRadians(alfaw1))), StartPointY - YEndLine1 - (D / 2) + (w1 * Math.cos(toRadians(alfaw1))) - (fix1 * Math.sin(toRadians(alfaw1))),Lprim);
        //////
        //////ترسیم خط کوچک بالا دومین خط x
        //////
        LineWithAlfaAndLength(180 + alfaw1, StartPointX - XEndLine1 - (w1 * Math.sin(toRadians(alfaw1))) - (fix1 * Math.cos(toRadians(alfaw1))), StartPointY - YEndLine1 - (D / 2) + (w1 * Math.cos(toRadians(alfaw1))) - (fix1 * Math.sin(toRadians(alfaw1))), x);
        //////
        /////ترسیم خط سوم
        //////
        xLine3 = (StartPointX - XEndLine1 - (w1 * Math.sin(toRadians(alfaw1))) - (fix1 * Math.cos(toRadians(alfaw1)))) - x * Math.cos(toRadians(alfaw1));
        yLine3 = (StartPointY - YEndLine1 - (D / 2) + (w1 * Math.cos(toRadians(alfaw1))) - (fix1 * Math.sin(toRadians(alfaw1)))) - x * Math.sin(toRadians(alfaw1));
        LprimLin3 = (yLine3 - y2Prim + (x2Prim - xLine3) * Math.tan(toRadians(alfa))) / (Math.cos(toRadians(alfaw1)) + Math.sin(toRadians(alfaw1)) * Math.tan(toRadians(alfa)));
        LineWithAlfaAndLength(alfaw1 - 90, xLine3, yLine3, LprimLin3);
        /////////////
        /////////////ترسیم خط hmin/3
        /////////////
        LineWithAlfaAndLength(180 + alfaw1, xLine3, yLine3, (hminw1 / 3));
        //LineWithAlfaAndLength(180 + alfaw1, xLine3, yLine3, (h / 3));
        ////////////
        ///////////ترسیم خط چهارم
        ///////////
        xLine4 = (StartPointX - XEndLine1 - (w1 * Math.sin(toRadians(alfaw1))) - (fix1 * Math.cos(toRadians(alfaw1)))) - x * Math.cos(toRadians(alfaw1)) - ((hminw1 / 3)) * Math.cos(toRadians(alfaw1));
        yLine4 = (StartPointY - YEndLine1 - (D / 2) + (w1 * Math.cos(toRadians(alfaw1))) - (fix1 * Math.sin(toRadians(alfaw1)))) - x * Math.sin(toRadians(alfaw1))- ((hminw1 / 3)) * Math.sin(toRadians(alfaw1));
        LprimLin4 = (yLine4 - y2Prim + (x2Prim - xLine4) * Math.tan(toRadians(alfa))) / (Math.cos(toRadians(alfaw1)) + Math.sin(toRadians(alfaw1)) * Math.tan(toRadians(alfa)));

        xLine41 = (StartPointX - XEndLine1 - (w1 * Math.sin(toRadians(alfaw1))) - (fix1 * Math.cos(toRadians(alfaw1)))) - x * Math.cos(toRadians(alfaw1)) - ((h / 3)) * Math.cos(toRadians(alfaw1));
        yLine41 = (StartPointY - YEndLine1 - (D / 2) + (w1 * Math.cos(toRadians(alfaw1))) - (fix1 * Math.sin(toRadians(alfaw1)))) - x * Math.sin(toRadians(alfaw1)) - ((h / 3)) * Math.sin(toRadians(alfaw1));

        xPoint1 = (StartPointX - XEndLine1 - (w1 * Math.sin(toRadians(alfaw1))) - (fix1 * Math.cos(toRadians(alfaw1)))) - x * Math.cos(toRadians(alfaw1)) - (((h) / 3)) * Math.cos(toRadians(alfaw1));
        yPoint1 = (StartPointY - YEndLine1 - (D / 2) + (w1 * Math.cos(toRadians(alfaw1))) - (fix1 * Math.sin(toRadians(alfaw1)))) - x * Math.sin(toRadians(alfaw1)) - (((h) / 3)) * Math.sin(toRadians(alfaw1));
        LprimLin5 = (yPoint1 - y2Prim + (x2Prim - xPoint1) * Math.tan(toRadians(alfa))) / (Math.cos(toRadians(alfaw1)) + Math.sin(toRadians(alfaw1)) * Math.tan(toRadians(alfa)));
        xPoint2 = xPoint1 + LprimLin5 * Math.sin(toRadians(alfaw1));
        yPoint2 = yPoint1 - LprimLin5 * Math.cos(toRadians(alfaw1));

        //LineWithAlfaAndLength(alfaw1 - 90, xLine41, yLine41, LprimLin3+50);

        DrawLineWithTwoPoint(xLine4, yLine4, xPoint2, yPoint2);

    }
}

/////سمت چپ پایین
function DrawDastakNew2(h, x, alfa, D, L, w2, b2, c1, c2, j, alfaw2, hminw2, t, StartPointX, StartPointY, Scale, Meyar) {
    h *= Scale; x *= Scale; D *= Scale; L *= Scale; w2 *= Scale;
    b2 *= Scale; c1 *= Scale; c2 *= Scale; hminw2 *= Scale; t *= Scale;
    fix1 = 0.35 * 100; fix1 *= Scale;
    fix2 = 0.25 * 100; fix2 *= Scale;
    XEndLine1 = (L / 2) * Math.cos(toRadians(alfa));
    YEndLine1 = (L / 2) * Math.sin(toRadians(alfa));

    LineWithAlfaAndLength(270-alfaw2 , StartPointX - XEndLine1, StartPointY + (D / 2), w2);

    T = (c1 + c2 + j) / Math.cos(toRadians(alfa));
    Q = fix1 / Math.sin(toRadians(alfaw2));
    if (Q <= T) {
        x1 = 0; y1 = Q;
        LineWithAlfaAndLength(90, StartPointX - XEndLine1 - (w2 * Math.sin(toRadians(alfaw2))), StartPointY + (D / 2) - (w2 * Math.cos(toRadians(alfaw2))), Q);
        LineWithAlfaAndLength(270 - alfaw2, StartPointX - XEndLine1 - x1, StartPointY + (D / 2) + y1, w2);
        LineWithAlfaAndLength(90, StartPointX - XEndLine1, StartPointY + YEndLine1 + (D / 2), Q);

        xX = StartPointX - XEndLine1 - (w2 * Math.sin(toRadians(alfaw2)));
        yX = StartPointY + YEndLine1 + (D / 2) - (w2 * Math.cos(toRadians(alfaw2))) + Q;
        LineWithAlfaAndLength(90, xX, yX, x / Math.sin(toRadians(alfaw2)));


        LineWithAlfaAndLength(-270 - alfaw2, xX, yX + (x / Math.sin(toRadians(alfaw2))), w2);
        /////خط دوم کوچک پایین
        LineWithAlfaAndLength(90, StartPointX - XEndLine1 - x1, StartPointY - YEndLine1 + (D / 2) + y1, x / Math.sin(toRadians(alfaw2)));
        ///////////خط کوچک بالا hmin
        xXhMin = xX;
        yXhMin = yX + (x / Math.sin(toRadians(alfaw2)));
        LineWithAlfaAndLength(90, xXhMin, yXhMin, (hminw2 / 3) / Math.sin(toRadians(alfaw2)));
        //////////
        /////خط سوم کوچک پایین
        LineWithAlfaAndLength(90, StartPointX - XEndLine1 - x1, StartPointY - YEndLine1 + (D / 2) + y1 + (x / Math.sin(toRadians(alfaw2))), ((h + t + hminw2) / 3) / Math.sin(toRadians(alfaw2)));
        ///////////
        ///////////خط چهارم
        xhMinEnd = xX;
        yhMinEnd = yX + (x / Math.sin(toRadians(alfaw2))) + ((hminw2 / 3) / Math.sin(toRadians(alfaw2)));

        xhMinEnd2 = StartPointX - XEndLine1 - x1;
        yhMinEnd2 = StartPointY + YEndLine1 + (D / 2) + y1 + (x / Math.sin(toRadians(alfaw2))) + (((h + t + hminw2) / 3) / Math.sin(toRadians(alfaw2)));

        DrawLineWithTwoPoint(xhMinEnd, yhMinEnd, xhMinEnd2, yhMinEnd2);
    }
    else {
        x1 = (-1) * fix1 * Math.cos(toRadians(alfaw2)) + (T - fix1 * Math.sin(toRadians(alfaw2))) * Math.tan(toRadians(alfaw2))
        y1 = T;

        LineWithAlfaAndLength(180-alfaw2, StartPointX - XEndLine1 - (w2 * Math.sin(toRadians(alfaw2))), StartPointY + (D / 2) - (w2 * Math.cos(toRadians(alfaw2))), fix1);

        x1Prim = StartPointX - XEndLine1 - (w2 * Math.sin(toRadians(alfaw2))) - (fix1 * Math.cos(toRadians(alfaw2)));
        y1Prim = StartPointY  + (D / 2) - (w2 * Math.cos(toRadians(alfaw2))) + (fix1 * Math.sin(toRadians(alfaw2)));
        //LineWithAlfaAndLength(90, x1Prim, y1Prim, 50);

        x2Prim = StartPointX;
        y2Prim = StartPointY + (D / 2) + ((c1 + c2) / Math.cos(toRadians(alfa)));
        //LineWithAlfaAndLength(90, x2Prim, y2Prim, 50);
        Lprim = (y1Prim - y2Prim + (x2Prim - x1Prim) * Math.tan(toRadians(alfa))) / (Math.cos(toRadians(alfaw2)) + Math.sin(toRadians(alfaw2)) * Math.tan(toRadians(alfa)));
        LineWithAlfaAndLength(270 - alfaw2, StartPointX - XEndLine1 - (w2 * Math.sin(toRadians(alfaw2))) - (fix1 * Math.cos(toRadians(alfaw2))), StartPointY + (D / 2) - (w2 * Math.cos(toRadians(alfaw2))) + (fix1 * Math.sin(toRadians(alfaw2))), Lprim);
        //////
        //////ترسیم خط کوچک بالا دومین خط x
        //////
        LineWithAlfaAndLength(180 - alfaw2, StartPointX - XEndLine1 - (w2 * Math.sin(toRadians(alfaw2))) - (fix1 * Math.cos(toRadians(alfaw2))), StartPointY + (D / 2) - (w2 * Math.cos(toRadians(alfaw2))) + (fix1 * Math.sin(toRadians(alfaw2))), x);
        //////
        /////ترسیم خط سوم
        //////
        xLine3 = (StartPointX - XEndLine1 - (w1 * Math.sin(toRadians(alfaw1))) - (fix1 * Math.cos(toRadians(alfaw1)))) - x * Math.cos(toRadians(alfaw1));
        yLine3 = (StartPointY - YEndLine1 - (D / 2) + (w1 * Math.cos(toRadians(alfaw1))) - (fix1 * Math.sin(toRadians(alfaw1)))) - x * Math.sin(toRadians(alfaw1));
        LprimLin3 = (yLine3 - y2Prim + (x2Prim - xLine3) * Math.tan(toRadians(alfa))) / (Math.cos(toRadians(alfaw1)) + Math.sin(toRadians(alfaw1)) * Math.tan(toRadians(alfa)));
        LineWithAlfaAndLength(alfaw1 - 90, xLine3, yLine3, LprimLin3);
        /////////////
        /////////////ترسیم خط hmin/3
        /////////////
        LineWithAlfaAndLength(180 + alfaw1, xLine3, yLine3, (hminw1 / 3) / Math.sin(toRadians(alfaw1)));
        ////////////
        ///////////ترسیم خط چهارم
        ///////////
        xLine4 = (StartPointX - XEndLine1 - (w1 * Math.sin(toRadians(alfaw1))) - (fix1 * Math.cos(toRadians(alfaw1)))) - x * Math.cos(toRadians(alfaw1)) - ((hminw1 / 3) / Math.sin(toRadians(alfaw1))) * Math.cos(toRadians(alfaw1));
        yLine4 = (StartPointY - YEndLine1 - (D / 2) + (w1 * Math.cos(toRadians(alfaw1))) - (fix1 * Math.sin(toRadians(alfaw1)))) - x * Math.sin(toRadians(alfaw1)) - ((hminw1 / 3) / Math.sin(toRadians(alfaw1))) * Math.sin(toRadians(alfaw1));
        LprimLin4 = (yLine4 - y2Prim + (x2Prim - xLine4) * Math.tan(toRadians(alfa))) / (Math.cos(toRadians(alfaw1)) + Math.sin(toRadians(alfaw1)) * Math.tan(toRadians(alfa)));
        xPoint1 = (StartPointX - XEndLine1 - (w1 * Math.sin(toRadians(alfaw1))) - (fix1 * Math.cos(toRadians(alfaw1)))) - x * Math.cos(toRadians(alfaw1)) - (((h + t + fix2) / 3) / Math.sin(toRadians(alfaw1))) * Math.cos(toRadians(alfaw1));
        yPoint1 = (StartPointY - YEndLine1 - (D / 2) + (w1 * Math.cos(toRadians(alfaw1))) - (fix1 * Math.sin(toRadians(alfaw1)))) - x * Math.sin(toRadians(alfaw1)) - (((h + t + fix2) / 3) / Math.sin(toRadians(alfaw1))) * Math.sin(toRadians(alfaw1));
        LprimLin5 = (yPoint1 - y2Prim + (x2Prim - xPoint1) * Math.tan(toRadians(alfa))) / (Math.cos(toRadians(alfaw1)) + Math.sin(toRadians(alfaw1)) * Math.tan(toRadians(alfa)));
        xPoint2 = xPoint1 + LprimLin5 * Math.sin(toRadians(alfaw1));
        yPoint2 = yPoint1 - LprimLin5 * Math.cos(toRadians(alfaw1));
        DrawLineWithTwoPoint(xLine4, yLine4, xPoint2, yPoint2);
    }
}

function DrawLineWithTwoPoint(x1, y1, x2, y2) {
    debugger
    var c = document.getElementById("myCanvas");
    var ctx = c.getContext("2d");
    ctx.moveTo(x1, y1);
    ctx.lineTo(x2, y2);
    ctx.stroke();
}

function LineWithAlfaAndLength(theta, x, y, Len) {
    debugger

    var c = document.getElementById("myCanvas");
    var ctx = c.getContext("2d");
    ctx.moveTo(x, y);
    xnew = x + Len * Math.cos(toRadians(theta));
    ynew = y + Len * Math.sin(toRadians(theta))
    ctx.lineTo(xnew, ynew);
    ctx.stroke();
}

function toRadians(angle) {
    return angle * (Math.PI / 180);
}
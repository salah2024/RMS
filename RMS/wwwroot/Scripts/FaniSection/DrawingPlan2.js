function DrawPlan(alfa, D, L, w1, w2, w3, w4, b2, c1, c2, j, alfaw1, alfaw2, alfaw3, alfaw4,
   StartPointX, StartPointY, Scale, Meyar) {

    D *= Scale; L *= Scale; w1 *= Scale; w2 *= Scale; w3 *= Scale; w4 *= Scale;
    b2 *= Scale; c1 *= Scale; c2 *= Scale;
    Tool = D + ((2 * c1) - (2 * j));

    ScaleNormal = (1 / Scale);

    LineWithAlfaAndLength(alfa, StartPointX, StartPointY - (D / Math.cos(toRadians(alfa)) / 2), L / 2);
    lineDistanceHPlan(alfa, StartPointX, StartPointY - (D / Math.cos(toRadians(alfa)) / 2), L / 2, D / Math.cos(toRadians(alfa)) / 2, 1, ScaleNormal, Meyar)

    XEndLine1 = (L / 2) * Math.cos(toRadians(alfa));
    YEndLine1 = (L / 2) * Math.sin(toRadians(alfa));
    LineWithAlfaAndLength(alfa, StartPointX, StartPointY + (D / Math.cos(toRadians(alfa)) / 2), L / 2);
    LineWithAlfaAndLength(alfa - 180, StartPointX, StartPointY - (D / Math.cos(toRadians(alfa)) / 2), L / 2);
    LineWithAlfaAndLength(alfa - 180, StartPointX, StartPointY + (D / Math.cos(toRadians(alfa)) / 2), L / 2);/////خط پایین اولی سمت چپ

    ///خط کنار وسط 
    LineWithAlfaAndLength(90, StartPointX + XEndLine1, StartPointY + YEndLine1 - (D / Math.cos(toRadians(alfa)) / 2), D / Math.cos(toRadians(alfa)));
    lineDistanceVPlan(90, StartPointX + XEndLine1, StartPointY + YEndLine1 - (D / Math.cos(toRadians(alfa)) / 2), D / Math.cos(toRadians(alfa)), 2, ScaleNormal, Meyar, 1)
    ///خط آکس 
    LineWithAlfaAndLengthDash(-90, StartPointX, StartPointY, (2 * D) / Math.cos(toRadians(alfa)));
    LineWithAlfaAndLengthDash(90, StartPointX, StartPointY, (2 * D) / Math.cos(toRadians(alfa)));
    ////////////
    LineWithAlfaAndLength(90, StartPointX - XEndLine1, StartPointY - YEndLine1 - (D / Math.cos(toRadians(alfa)) / 2), D / Math.cos(toRadians(alfa)))
    ////////////
    LineWithAlfaAndLength(alfa - 180, StartPointX + XEndLine1, StartPointY + YEndLine1 - (D / Math.cos(toRadians(alfa)) / 2) - (c1 / Math.cos(toRadians(alfa))), L)
    ////خط اول افقی بالا
    LineWithAlfaAndLength(alfa - 180, StartPointX + XEndLine1, StartPointY + YEndLine1 - (D / Math.cos(toRadians(alfa)) / 2) - ((c1 + c2) / Math.cos(toRadians(alfa))), L)
    lineDistanceHPlan(alfa - 180, StartPointX + XEndLine1, StartPointY + YEndLine1 - (D / Math.cos(toRadians(alfa)) / 2) - ((c1 + c2) / Math.cos(toRadians(alfa))), L, -10, 2, ScaleNormal, Meyar)
    //

    ///خط کنار کوچک دوم
    LineWithAlfaAndLength(90, StartPointX + XEndLine1, StartPointY + YEndLine1 - (D / Math.cos(toRadians(alfa)) / 2) - (c1 / Math.cos(toRadians(alfa))), c1 / Math.cos(toRadians(alfa)))
    lineDistanceVPlan(90, StartPointX + XEndLine1, StartPointY + YEndLine1 - (D / Math.cos(toRadians(alfa)) / 2) - (c1 / Math.cos(toRadians(alfa))), c1 / Math.cos(toRadians(alfa)), 2, ScaleNormal, Meyar, 1)
    ///خط کنار کوچک اول
    LineWithAlfaAndLength(90, StartPointX + XEndLine1, StartPointY + YEndLine1 - (D / Math.cos(toRadians(alfa)) / 2) - ((c1 + c2) / Math.cos(toRadians(alfa))), (c2) / Math.cos(toRadians(alfa)))
    lineDistanceVPlan(90, StartPointX + XEndLine1, StartPointY + YEndLine1 - (D / Math.cos(toRadians(alfa)) / 2) - ((c1 + c2) / Math.cos(toRadians(alfa))), c2 / Math.cos(toRadians(alfa)), 2, ScaleNormal, Meyar, 1)

    LineWithAlfaAndLength(90, StartPointX - XEndLine1, StartPointY - YEndLine1 - (D / Math.cos(toRadians(alfa)) / 2) - (c1 / Math.cos(toRadians(alfa))), c1 / Math.cos(toRadians(alfa)))
    LineWithAlfaAndLength(90, StartPointX - XEndLine1, StartPointY - YEndLine1 - (D / Math.cos(toRadians(alfa)) / 2) - ((c1 + c2) / Math.cos(toRadians(alfa))), (c2) / Math.cos(toRadians(alfa)))

    LineWithAlfaAndLength(alfa, StartPointX, StartPointY + (D / Math.cos(toRadians(alfa)) / 2) + (c1 / Math.cos(toRadians(alfa))), L / 2);
    LineWithAlfaAndLength(alfa - 180, StartPointX, StartPointY + (D / Math.cos(toRadians(alfa)) / 2) + (c1 / Math.cos(toRadians(alfa))), L / 2);

    LineWithAlfaAndLength(alfa, StartPointX, StartPointY + (D / Math.cos(toRadians(alfa)) / 2) + ((c1 + c2) / Math.cos(toRadians(alfa))), L / 2);
    LineWithAlfaAndLength(alfa - 180, StartPointX, StartPointY + (D / Math.cos(toRadians(alfa)) / 2) + ((c1 + c2) / Math.cos(toRadians(alfa))), L / 2);


    LineWithAlfaAndLength(90, StartPointX + (L / 2) * Math.cos(toRadians(alfa)), StartPointY + (D / Math.cos(toRadians(alfa)) / 2) + (L / 2) * Math.sin(toRadians(alfa)), c1 / Math.cos(toRadians(alfa)));
    LineWithAlfaAndLength(90, StartPointX + (L / 2) * Math.cos(toRadians(alfa)), StartPointY + (D / Math.cos(toRadians(alfa)) / 2) + (c1 / Math.cos(toRadians(alfa))) + (L / 2) * Math.sin(toRadians(alfa)), c2 / Math.cos(toRadians(alfa)));

    LineWithAlfaAndLength(90, StartPointX - (L / 2) * Math.cos(toRadians(alfa)), StartPointY + (D / Math.cos(toRadians(alfa)) / 2) - (L / 2) * Math.sin(toRadians(alfa)), c1 / Math.cos(toRadians(alfa)));
    LineWithAlfaAndLength(90, StartPointX - (L / 2) * Math.cos(toRadians(alfa)), StartPointY + (D / Math.cos(toRadians(alfa)) / 2) + (c1 / Math.cos(toRadians(alfa))) - (L / 2) * Math.sin(toRadians(alfa)), c2 / Math.cos(toRadians(alfa)));
}

function DrawPlan2Dahaneh(alfa, D, L, w1, w2, w3, w4, b2, c1, c2, j, alfaw1, alfaw2, alfaw3, alfaw4,p2,
   StartPointX, StartPointY, Scale, Meyar) {

    D *= Scale; L *= Scale; w1 *= Scale; w2 *= Scale; w3 *= Scale; w4 *= Scale;
    b2 *= Scale; c1 *= Scale; c2 *= Scale; p2 *= Scale;
    Tool = D + ((2 * c1) - (2 * j));

    ScaleNormal = (1 / Scale);

    LineWithAlfaAndLength(alfa, StartPointX, StartPointY - (D / Math.cos(toRadians(alfa)) / 2), L / 2);
    lineDistanceHPlan(alfa, StartPointX, StartPointY - (D / Math.cos(toRadians(alfa)) / 2), L / 2, D / Math.cos(toRadians(alfa)) / 2, 1, ScaleNormal, Meyar)

    XEndLine1 = (L / 2) * Math.cos(toRadians(alfa));
    YEndLine1 = (L / 2) * Math.sin(toRadians(alfa));
    LineWithAlfaAndLength(alfa - 180, StartPointX, StartPointY - (D / Math.cos(toRadians(alfa)) / 2), L / 2);

    ///خط کنار وسط 
    LineWithAlfaAndLength(90, StartPointX + XEndLine1, StartPointY + YEndLine1 - (D / Math.cos(toRadians(alfa)) / 2), D / Math.cos(toRadians(alfa)));
    lineDistanceVPlan(90, StartPointX + XEndLine1, StartPointY + YEndLine1 - (D / Math.cos(toRadians(alfa)) / 2), D / Math.cos(toRadians(alfa)), 2, ScaleNormal, Meyar, 1)
    LineWithAlfaAndLength(90, StartPointX - XEndLine1, StartPointY - YEndLine1 - (D / Math.cos(toRadians(alfa)) / 2), D / Math.cos(toRadians(alfa)))
    LineWithAlfaAndLength(alfa - 180, StartPointX + XEndLine1, StartPointY + YEndLine1 - (D / Math.cos(toRadians(alfa)) / 2) - (c1 / Math.cos(toRadians(alfa))), L)
    ////خط اول افقی بالا
    LineWithAlfaAndLength(alfa - 180, StartPointX + XEndLine1, StartPointY + YEndLine1 - (D / Math.cos(toRadians(alfa)) / 2) - ((c1 + c2) / Math.cos(toRadians(alfa))), L)
    lineDistanceHPlan(alfa - 180, StartPointX + XEndLine1, StartPointY + YEndLine1 - (D / Math.cos(toRadians(alfa)) / 2) - ((c1 + c2) / Math.cos(toRadians(alfa))), L, -10, 2, ScaleNormal, Meyar)
    ///خط آکس 
    LineWithAlfaAndLengthDash(-90, StartPointX, StartPointY, (2 * D) / Math.cos(toRadians(alfa)));
    LineWithAlfaAndLengthDash(90, StartPointX, StartPointY, (3 * D) / Math.cos(toRadians(alfa)));
    //
    ///خط کنار کوچک دوم
    LineWithAlfaAndLength(90, StartPointX + XEndLine1, StartPointY + YEndLine1 - (D / Math.cos(toRadians(alfa)) / 2) - (c1 / Math.cos(toRadians(alfa))), c1 / Math.cos(toRadians(alfa)))
    lineDistanceVPlan(90, StartPointX + XEndLine1, StartPointY + YEndLine1 - (D / Math.cos(toRadians(alfa)) / 2) - (c1 / Math.cos(toRadians(alfa))), c1 / Math.cos(toRadians(alfa)), 2, ScaleNormal, Meyar, 1)
    ///خط کنار کوچک اول
    LineWithAlfaAndLength(90, StartPointX + XEndLine1, StartPointY + YEndLine1 - (D / Math.cos(toRadians(alfa)) / 2) - ((c1 + c2) / Math.cos(toRadians(alfa))), (c2) / Math.cos(toRadians(alfa)))
    lineDistanceVPlan(90, StartPointX + XEndLine1, StartPointY + YEndLine1 - (D / Math.cos(toRadians(alfa)) / 2) - ((c1 + c2) / Math.cos(toRadians(alfa))), c2 / Math.cos(toRadians(alfa)), 2, ScaleNormal, Meyar, 1)
    LineWithAlfaAndLength(90, StartPointX - XEndLine1, StartPointY - YEndLine1 - (D / Math.cos(toRadians(alfa)) / 2) - (c1 / Math.cos(toRadians(alfa))), c1 / Math.cos(toRadians(alfa)))
    LineWithAlfaAndLength(90, StartPointX - XEndLine1, StartPointY - YEndLine1 - (D / Math.cos(toRadians(alfa)) / 2) - ((c1 + c2) / Math.cos(toRadians(alfa))), c2 / Math.cos(toRadians(alfa)))
    //دو خط اول قسمت پایین دهنه اول
    LineWithAlfaAndLength(alfa - 180, StartPointX, StartPointY + (D / Math.cos(toRadians(alfa)) / 2), L / 2);/////خط پایین اولی سمت چپ
    LineWithAlfaAndLength(alfa, StartPointX, StartPointY + (D / Math.cos(toRadians(alfa)) / 2), L / 2);
    ////کشیدن p2 سمت راست
    LineWithAlfaAndLength(90, StartPointX + XEndLine1, StartPointY + YEndLine1 + (D / Math.cos(toRadians(alfa)) / 2), p2 / Math.cos(toRadians(alfa)))
    lineDistanceVPlan(90, StartPointX + XEndLine1, StartPointY + YEndLine1 + (D / Math.cos(toRadians(alfa)) / 2), p2 / Math.cos(toRadians(alfa)), 2, ScaleNormal, Meyar, 1)
    ////کشیدن p2 سمت چپ
    LineWithAlfaAndLength(90, StartPointX - XEndLine1, StartPointY - YEndLine1 + (D / Math.cos(toRadians(alfa)) / 2), p2 / Math.cos(toRadians(alfa)))
    //دو خط دوم قسمت پایین دهنه اول
    LineWithAlfaAndLength(alfa - 180, StartPointX, StartPointY + (D / Math.cos(toRadians(alfa)) / 2) + p2 / Math.cos(toRadians(alfa)), L / 2);/////خط پایین اولی سمت چپ
    LineWithAlfaAndLength(alfa, StartPointX, StartPointY + (D / Math.cos(toRadians(alfa)) / 2) + p2 / Math.cos(toRadians(alfa)), L / 2);
    ///خط کنار وسط دهنه دوم 
    LineWithAlfaAndLength(90, StartPointX + XEndLine1, StartPointY + YEndLine1 + D / Math.cos(toRadians(alfa)) + p2 / Math.cos(toRadians(alfa)) - (D / Math.cos(toRadians(alfa)) / 2), D / Math.cos(toRadians(alfa)));
    LineWithAlfaAndLength(90, StartPointX - XEndLine1, StartPointY - YEndLine1 + D / Math.cos(toRadians(alfa)) + p2 / Math.cos(toRadians(alfa)) - (D / Math.cos(toRadians(alfa)) / 2), D / Math.cos(toRadians(alfa)));

    LineWithAlfaAndLength(alfa, StartPointX, StartPointY + (D / Math.cos(toRadians(alfa)) / 2) + D / Math.cos(toRadians(alfa)) + p2 / Math.cos(toRadians(alfa)) + (c1 / Math.cos(toRadians(alfa))), L / 2);
    LineWithAlfaAndLength(alfa - 180, StartPointX, StartPointY + (D / Math.cos(toRadians(alfa)) / 2) + D / Math.cos(toRadians(alfa)) + p2 / Math.cos(toRadians(alfa)) + (c1 / Math.cos(toRadians(alfa))), L / 2);
    LineWithAlfaAndLength(alfa, StartPointX, StartPointY + (D / Math.cos(toRadians(alfa)) / 2) + D / Math.cos(toRadians(alfa)) + p2 / Math.cos(toRadians(alfa)) + ((c1 + c2) / Math.cos(toRadians(alfa))), L / 2);
    LineWithAlfaAndLength(alfa - 180, StartPointX, StartPointY + (D / Math.cos(toRadians(alfa)) / 2) + D / Math.cos(toRadians(alfa)) + p2 / Math.cos(toRadians(alfa)) + ((c1 + c2) / Math.cos(toRadians(alfa))), L / 2);
    LineWithAlfaAndLength(90, StartPointX + (L / 2) * Math.cos(toRadians(alfa)), StartPointY + (D / Math.cos(toRadians(alfa)) / 2) + D / Math.cos(toRadians(alfa)) + p2 / Math.cos(toRadians(alfa)) + (L / 2) * Math.sin(toRadians(alfa)), c1 / Math.cos(toRadians(alfa)));
    LineWithAlfaAndLength(90, StartPointX + (L / 2) * Math.cos(toRadians(alfa)), StartPointY + (D / Math.cos(toRadians(alfa)) / 2) + D / Math.cos(toRadians(alfa)) + p2 / Math.cos(toRadians(alfa)) + (c1 / Math.cos(toRadians(alfa))) + (L / 2) * Math.sin(toRadians(alfa)), c2 / Math.cos(toRadians(alfa)));
    LineWithAlfaAndLength(90, StartPointX - (L / 2) * Math.cos(toRadians(alfa)), StartPointY + (D / Math.cos(toRadians(alfa)) / 2) + D / Math.cos(toRadians(alfa)) + p2 / Math.cos(toRadians(alfa)) - (L / 2) * Math.sin(toRadians(alfa)), c1 / Math.cos(toRadians(alfa)));
    LineWithAlfaAndLength(90, StartPointX - (L / 2) * Math.cos(toRadians(alfa)), StartPointY + (D / Math.cos(toRadians(alfa)) / 2) + D / Math.cos(toRadians(alfa)) + p2 / Math.cos(toRadians(alfa)) + (c1 / Math.cos(toRadians(alfa))) - (L / 2) * Math.sin(toRadians(alfa)), c2 / Math.cos(toRadians(alfa)));
    //دو خط اول قسمت پایین دهنه دوم
    LineWithAlfaAndLength(alfa - 180, StartPointX, StartPointY + (D / Math.cos(toRadians(alfa)) / 2) + D / Math.cos(toRadians(alfa)) + p2 / Math.cos(toRadians(alfa)), L / 2);/////خط پایین اولی سمت چپ
    LineWithAlfaAndLength(alfa, StartPointX, StartPointY + (D / Math.cos(toRadians(alfa)) / 2) + D / Math.cos(toRadians(alfa)) + p2 / Math.cos(toRadians(alfa)), L / 2);
}


function DrawPlan3Dahaneh(alfa, D, L, w1, w2, w3, w4, b2, c1, c2, j, alfaw1, alfaw2, alfaw3, alfaw4, p2,
   StartPointX, StartPointY, Scale, Meyar) {

    D *= Scale; L *= Scale; w1 *= Scale; w2 *= Scale; w3 *= Scale; w4 *= Scale;
    b2 *= Scale; c1 *= Scale; c2 *= Scale; p2 *= Scale;
    Tool = D + ((2 * c1) - (2 * j));

    ScaleNormal = (1 / Scale);

    LineWithAlfaAndLength(alfa, StartPointX, StartPointY - (D / Math.cos(toRadians(alfa)) / 2), L / 2);
    lineDistanceHPlan(alfa, StartPointX, StartPointY - (D / Math.cos(toRadians(alfa)) / 2), L / 2, D / Math.cos(toRadians(alfa)) / 2, 1, ScaleNormal, Meyar)

    XEndLine1 = (L / 2) * Math.cos(toRadians(alfa));
    YEndLine1 = (L / 2) * Math.sin(toRadians(alfa));
    LineWithAlfaAndLength(alfa - 180, StartPointX, StartPointY - (D / Math.cos(toRadians(alfa)) / 2), L / 2);

    ///خط کنار وسط 
    LineWithAlfaAndLength(90, StartPointX + XEndLine1, StartPointY + YEndLine1 - (D / Math.cos(toRadians(alfa)) / 2), D / Math.cos(toRadians(alfa)));
    lineDistanceVPlan(90, StartPointX + XEndLine1, StartPointY + YEndLine1 - (D / Math.cos(toRadians(alfa)) / 2), D / Math.cos(toRadians(alfa)), 2, ScaleNormal, Meyar, 1)
    LineWithAlfaAndLength(90, StartPointX - XEndLine1, StartPointY - YEndLine1 - (D / Math.cos(toRadians(alfa)) / 2), D / Math.cos(toRadians(alfa)))
    LineWithAlfaAndLength(alfa - 180, StartPointX + XEndLine1, StartPointY + YEndLine1 - (D / Math.cos(toRadians(alfa)) / 2) - (c1 / Math.cos(toRadians(alfa))), L)
    ///خط آکس 
    LineWithAlfaAndLengthDash(-90, StartPointX, StartPointY, (2 * D) / Math.cos(toRadians(alfa)));
    LineWithAlfaAndLengthDash(90, StartPointX, StartPointY, (5 * D) / Math.cos(toRadians(alfa)));

    ////خط اول افقی بالا
    LineWithAlfaAndLength(alfa - 180, StartPointX + XEndLine1, StartPointY + YEndLine1 - (D / Math.cos(toRadians(alfa)) / 2) - ((c1 + c2) / Math.cos(toRadians(alfa))), L)
    lineDistanceHPlan(alfa - 180, StartPointX + XEndLine1, StartPointY + YEndLine1 - (D / Math.cos(toRadians(alfa)) / 2) - ((c1 + c2) / Math.cos(toRadians(alfa))), L, -10, 2, ScaleNormal, Meyar)
    //
    ///خط کنار کوچک دوم
    LineWithAlfaAndLength(90, StartPointX + XEndLine1, StartPointY + YEndLine1 - (D / Math.cos(toRadians(alfa)) / 2) - (c1 / Math.cos(toRadians(alfa))), c1 / Math.cos(toRadians(alfa)))
    lineDistanceVPlan(90, StartPointX + XEndLine1, StartPointY + YEndLine1 - (D / Math.cos(toRadians(alfa)) / 2) - (c1 / Math.cos(toRadians(alfa))), c1 / Math.cos(toRadians(alfa)), 2, ScaleNormal, Meyar, 1)
    ///خط کنار کوچک اول
    LineWithAlfaAndLength(90, StartPointX + XEndLine1, StartPointY + YEndLine1 - (D / Math.cos(toRadians(alfa)) / 2) - ((c1 + c2) / Math.cos(toRadians(alfa))), (c2) / Math.cos(toRadians(alfa)))
    lineDistanceVPlan(90, StartPointX + XEndLine1, StartPointY + YEndLine1 - (D / Math.cos(toRadians(alfa)) / 2) - ((c1 + c2) / Math.cos(toRadians(alfa))), c2 / Math.cos(toRadians(alfa)), 2, ScaleNormal, Meyar, 1)
    LineWithAlfaAndLength(90, StartPointX - XEndLine1, StartPointY - YEndLine1 - (D / Math.cos(toRadians(alfa)) / 2) - (c1 / Math.cos(toRadians(alfa))), c1 / Math.cos(toRadians(alfa)))
    LineWithAlfaAndLength(90, StartPointX - XEndLine1, StartPointY - YEndLine1 - (D / Math.cos(toRadians(alfa)) / 2) - ((c1 + c2) / Math.cos(toRadians(alfa))), c2 / Math.cos(toRadians(alfa)))
    //دو خط اول قسمت پایین دهنه اول
    LineWithAlfaAndLength(alfa - 180, StartPointX, StartPointY + (D / Math.cos(toRadians(alfa)) / 2), L / 2);/////خط پایین اولی سمت چپ
    LineWithAlfaAndLength(alfa, StartPointX, StartPointY + (D / Math.cos(toRadians(alfa)) / 2), L / 2);
    ////کشیدن p2 سمت راست
    LineWithAlfaAndLength(90, StartPointX + XEndLine1, StartPointY + YEndLine1 + (D / Math.cos(toRadians(alfa)) / 2), p2 / Math.cos(toRadians(alfa)))
    ////کشیدن p2 سمت چپ
    LineWithAlfaAndLength(90, StartPointX - XEndLine1, StartPointY - YEndLine1 + (D / Math.cos(toRadians(alfa)) / 2), p2 / Math.cos(toRadians(alfa)))
    //دو خط دوم قسمت پایین دهنه اول
    LineWithAlfaAndLength(alfa - 180, StartPointX, StartPointY + (D / Math.cos(toRadians(alfa)) / 2) + p2 / Math.cos(toRadians(alfa)), L / 2);/////خط پایین اولی سمت چپ
    LineWithAlfaAndLength(alfa, StartPointX, StartPointY + (D / Math.cos(toRadians(alfa)) / 2) + p2 / Math.cos(toRadians(alfa)), L / 2);
    ///خط کنار وسط دهنه دوم 
    LineWithAlfaAndLength(90, StartPointX + XEndLine1, StartPointY + YEndLine1 + D / Math.cos(toRadians(alfa)) + p2 / Math.cos(toRadians(alfa)) - (D / Math.cos(toRadians(alfa)) / 2), D / Math.cos(toRadians(alfa)));
    LineWithAlfaAndLength(90, StartPointX - XEndLine1, StartPointY - YEndLine1 + D / Math.cos(toRadians(alfa)) + p2 / Math.cos(toRadians(alfa)) - (D / Math.cos(toRadians(alfa)) / 2), D / Math.cos(toRadians(alfa)));

    //دو خط اول قسمت پایین دهنه دوم
    LineWithAlfaAndLength(alfa - 180, StartPointX, StartPointY + (D / Math.cos(toRadians(alfa)) / 2) + D / Math.cos(toRadians(alfa)) + p2 / Math.cos(toRadians(alfa)), L / 2);/////خط پایین اولی سمت چپ
    LineWithAlfaAndLength(alfa, StartPointX, StartPointY + (D / Math.cos(toRadians(alfa)) / 2) + D / Math.cos(toRadians(alfa)) + p2 / Math.cos(toRadians(alfa)), L / 2);
    ////کشیدن p2 سمت راست دهنه دوم
    LineWithAlfaAndLength(90, StartPointX + XEndLine1, StartPointY + YEndLine1 + (D / Math.cos(toRadians(alfa)) / 2) + D / Math.cos(toRadians(alfa)) + p2 / Math.cos(toRadians(alfa)), p2 / Math.cos(toRadians(alfa)))
    ////کشیدن p2 سمت چپ دهنه دوم
    LineWithAlfaAndLength(90, StartPointX - XEndLine1, StartPointY - YEndLine1 + (D / Math.cos(toRadians(alfa)) / 2) + D / Math.cos(toRadians(alfa)) + p2 / Math.cos(toRadians(alfa)), p2 / Math.cos(toRadians(alfa)))
    //دو خط دوم قسمت پایین دهنه دوم
    LineWithAlfaAndLength(alfa - 180, StartPointX, StartPointY + (D / Math.cos(toRadians(alfa)) / 2) + 2 * p2 / Math.cos(toRadians(alfa)) + D / Math.cos(toRadians(alfa)), L / 2);/////خط پایین اولی سمت چپ
    LineWithAlfaAndLength(alfa, StartPointX, StartPointY + (D / Math.cos(toRadians(alfa)) / 2) + 2 * p2 / Math.cos(toRadians(alfa)) + D / Math.cos(toRadians(alfa)), L / 2);
    ///خط کنار وسط دهنه سوم 
    LineWithAlfaAndLength(90, StartPointX + XEndLine1, StartPointY + YEndLine1 + 2 * D / Math.cos(toRadians(alfa)) + 2 * p2 / Math.cos(toRadians(alfa)) - (D / Math.cos(toRadians(alfa)) / 2), D / Math.cos(toRadians(alfa)));
    LineWithAlfaAndLength(90, StartPointX - XEndLine1, StartPointY - YEndLine1 + 2 * D / Math.cos(toRadians(alfa)) + 2 * p2 / Math.cos(toRadians(alfa)) - (D / Math.cos(toRadians(alfa)) / 2), D / Math.cos(toRadians(alfa)));


    ///////////
    LineWithAlfaAndLength(alfa, StartPointX, StartPointY + (D / Math.cos(toRadians(alfa)) / 2) + 2 * D / Math.cos(toRadians(alfa)) + 2 * p2 / Math.cos(toRadians(alfa)) + (c1 / Math.cos(toRadians(alfa))), L / 2);
    LineWithAlfaAndLength(alfa - 180, StartPointX, StartPointY + (D / Math.cos(toRadians(alfa)) / 2) + 2 * D / Math.cos(toRadians(alfa)) + 2 * p2 / Math.cos(toRadians(alfa)) + (c1 / Math.cos(toRadians(alfa))), L / 2);
    LineWithAlfaAndLength(alfa, StartPointX, StartPointY + (D / Math.cos(toRadians(alfa)) / 2) + 2 * D / Math.cos(toRadians(alfa)) + 2 * p2 / Math.cos(toRadians(alfa)) + ((c1 + c2) / Math.cos(toRadians(alfa))), L / 2);
    LineWithAlfaAndLength(alfa - 180, StartPointX, StartPointY + (D / Math.cos(toRadians(alfa)) / 2) + 2 * D / Math.cos(toRadians(alfa)) + 2 * p2 / Math.cos(toRadians(alfa)) + ((c1 + c2) / Math.cos(toRadians(alfa))), L / 2);
    LineWithAlfaAndLength(90, StartPointX + (L / 2) * Math.cos(toRadians(alfa)), StartPointY + (D / Math.cos(toRadians(alfa)) / 2) + 2 * D / Math.cos(toRadians(alfa)) + 2 * p2 / Math.cos(toRadians(alfa)) + (L / 2) * Math.sin(toRadians(alfa)), c1 / Math.cos(toRadians(alfa)));
    LineWithAlfaAndLength(90, StartPointX + (L / 2) * Math.cos(toRadians(alfa)), StartPointY + (D / Math.cos(toRadians(alfa)) / 2) + 2 * D / Math.cos(toRadians(alfa)) + 2 * p2 / Math.cos(toRadians(alfa)) + (c1 / Math.cos(toRadians(alfa))) + (L / 2) * Math.sin(toRadians(alfa)), c2 / Math.cos(toRadians(alfa)));
    LineWithAlfaAndLength(90, StartPointX - (L / 2) * Math.cos(toRadians(alfa)), StartPointY + (D / Math.cos(toRadians(alfa)) / 2) + 2 * D / Math.cos(toRadians(alfa)) + 2 * p2 / Math.cos(toRadians(alfa)) - (L / 2) * Math.sin(toRadians(alfa)), c1 / Math.cos(toRadians(alfa)));
    LineWithAlfaAndLength(90, StartPointX - (L / 2) * Math.cos(toRadians(alfa)), StartPointY + (D / Math.cos(toRadians(alfa)) / 2) + 2 * D / Math.cos(toRadians(alfa)) + 2 * p2 / Math.cos(toRadians(alfa)) + (c1 / Math.cos(toRadians(alfa))) - (L / 2) * Math.sin(toRadians(alfa)), c2 / Math.cos(toRadians(alfa)));
    //دو خط اول قسمت پایین دهنه دوم
    LineWithAlfaAndLength(alfa - 180, StartPointX, StartPointY + (D / Math.cos(toRadians(alfa)) / 2) + 2 * D / Math.cos(toRadians(alfa)) + 2 * p2 / Math.cos(toRadians(alfa)), L / 2);/////خط پایین اولی سمت چپ
    LineWithAlfaAndLength(alfa, StartPointX, StartPointY + (D / Math.cos(toRadians(alfa)) / 2) + 2 * D / Math.cos(toRadians(alfa)) + 2 * p2 / Math.cos(toRadians(alfa)), L / 2);
}

function DrawDastakNew1(h, x, alfa, D, L, w1, b2, c1, c2, j, alfaw1, hminw1, t, StartPointX, StartPointY, Scale, Meyar) {
    h *= Scale; x *= Scale; D *= Scale; L *= Scale; w1 *= Scale;
    b2 *= Scale; c1 *= Scale; c2 *= Scale; hminw1 *= Scale; t *= Scale;
    fix1 = 0.35 * 100; fix1 *= Scale;
    fix2 = 0.25 * 100; fix2 *= Scale;
    h += t;
    ScaleNormal = (1 / Scale);
    XEndLine1 = (L / 2) * Math.cos(toRadians(alfa));
    YEndLine1 = (L / 2) * Math.sin(toRadians(alfa));
    LineWithAlfaAndLengthDastak(90 + alfaw1, StartPointX - XEndLine1, StartPointY - YEndLine1 - (D / Math.cos(toRadians(alfa)) / 2), w1);
    lineDistanceHPlan(90 + alfaw1, StartPointX - XEndLine1, StartPointY - YEndLine1 - (D / Math.cos(toRadians(alfa)) / 2), w1, 10, 2, ScaleNormal, Meyar, 2)

    LW1j = w1/100;
    $('#HDFLW1j').val((parseFloat(LW1j) * parseFloat(ScaleNormal)).toFixed(3));

    T = (c1 + c2 + j) / Math.cos(toRadians(alfa));
    Q = fix1 / Math.sin(toRadians(alfaw1));
    if (Q <= T) {
        x1 = 0; y1 = Q;
        LineWithAlfaAndLengthDastak(-90, StartPointX - XEndLine1 - (w1 * Math.sin(toRadians(alfaw1))), StartPointY - YEndLine1 - (D / Math.cos(toRadians(alfa)) / 2) + (w1 * Math.cos(toRadians(alfaw1))), Q);
        lineDistanceVPlan(-90, StartPointX - XEndLine1 - (w1 * Math.sin(toRadians(alfaw1))), StartPointY - YEndLine1 - (D / Math.cos(toRadians(alfa)) / 2) + (w1 * Math.cos(toRadians(alfaw1))), Q, 1, ScaleNormal, Meyar, 2)

        LineWithAlfaAndLengthDastak(90 + alfaw1, StartPointX - XEndLine1 - x1, StartPointY - YEndLine1 - (D / Math.cos(toRadians(alfa)) / 2) - y1, w1);
        LineWithAlfaAndLengthDastak(-90, StartPointX - XEndLine1, StartPointY - YEndLine1 - (D / Math.cos(toRadians(alfa)) / 2), Q);

        xX = StartPointX - XEndLine1 - (w1 * Math.sin(toRadians(alfaw1)));
        yX = StartPointY - YEndLine1 - (D / Math.cos(toRadians(alfa)) / 2) + (w1 * Math.cos(toRadians(alfaw1))) - Q;
        LineWithAlfaAndLengthDastak(-90, xX, yX, x / Math.sin(toRadians(alfaw1)));
        lineDistanceVPlan(-90, xX, yX, x / Math.sin(toRadians(alfaw1)), 1, ScaleNormal, Meyar, 2)

        LineWithAlfaAndLengthDastak(-90 + alfaw1, xX, yX - (x / Math.sin(toRadians(alfaw1))), w1);
        /////خط دوم کوچک پایین
        LineWithAlfaAndLengthDastak(-90, StartPointX - XEndLine1 - x1, StartPointY - YEndLine1 - (D / Math.cos(toRadians(alfa)) / 2) - y1, x / Math.sin(toRadians(alfaw1)));

        ///////////خط کوچک بالا hmin
        xXhMin = xX;
        yXhMin = yX - (x / Math.sin(toRadians(alfaw1)));
        LineWithAlfaAndLengthDastak(-90, xXhMin, yXhMin, (hminw1 / 3) / Math.sin(toRadians(alfaw1)));
        lineDistanceVPlan(-90, xXhMin, yXhMin, (hminw1 / 3) / Math.sin(toRadians(alfaw1)), 1, ScaleNormal, Meyar, 2)

        ///////////خط چهارم
        xsefr1 = (StartPointX - XEndLine1) + (fix1 + x + (h / 3)) * Math.cos(toRadians(alfaw1 - 180));
        ysefr1 = (StartPointY - YEndLine1 - (D / Math.cos(toRadians(alfa)) / 2)) + (fix1 + x + (h / 3)) * Math.sin(toRadians(alfaw1 - 180));
        xsefr2 = xX;
        ysefr2 = yXhMin - ((hminw1 / 3) / Math.sin(toRadians(alfaw1)));
        Y = ((ysefr2 - ysefr1) / (xsefr2 - xsefr1)) * ((StartPointX - XEndLine1) - xsefr1) + (ysefr1);
        DrawLineWithTwoPointDastak((StartPointX - XEndLine1), Y, xX, ysefr2);
        DrawLineWithTwoPointDastak((StartPointX - XEndLine1), (StartPointY - YEndLine1 - (D / Math.cos(toRadians(alfa)) / 2)), (StartPointX - XEndLine1), Y);

        LW1p = calOfTwoPoint((StartPointX - XEndLine1), Y, xX, ysefr2);
        LB1W1 = calOfTwoPoint(StartPointX - XEndLine1 - (w1 * Math.sin(toRadians(alfaw1))), StartPointY - YEndLine1 - (D / Math.cos(toRadians(alfa)) / 2) + (w1 * Math.cos(toRadians(alfaw1))), xX, ysefr2);
        LB2W1 = calOfTwoPoint(StartPointX - XEndLine1, StartPointY - YEndLine1 - (D / Math.cos(toRadians(alfa)) / 2) - ((c1 + c2) / Math.cos(toRadians(alfa))), (StartPointX - XEndLine1), Y);

        $('#HDFLW1p').val((parseFloat(LW1p)*parseFloat(ScaleNormal)).toFixed(3));
        $('#HDFLB1W1').val((parseFloat(LB1W1) * parseFloat(ScaleNormal)).toFixed(3));
        $('#HDFLB2W1').val((parseFloat(LB2W1) * parseFloat(ScaleNormal)).toFixed(3));

    }
    else {
        x1 = (-1) * fix1 * Math.cos(toRadians(alfaw1)) + (T - fix1 * Math.sin(toRadians(alfaw1))) * Math.tan(toRadians(alfaw1))
        y1 = T;

        LineWithAlfaAndLengthDastak(180 + alfaw1, StartPointX - XEndLine1 - (w1 * Math.sin(toRadians(alfaw1))), StartPointY - YEndLine1 - (D / Math.cos(toRadians(alfa)) / 2) + (w1 * Math.cos(toRadians(alfaw1))), fix1);

        x1Prim = StartPointX - XEndLine1 - (w1 * Math.sin(toRadians(alfaw1))) - (fix1 * Math.cos(toRadians(alfaw1)));
        y1Prim = StartPointY - YEndLine1 - (D / Math.cos(toRadians(alfa)) / 2) + (w1 * Math.cos(toRadians(alfaw1))) - (fix1 * Math.sin(toRadians(alfaw1)))

        x2Prim = StartPointX + XEndLine1;
        y2Prim = StartPointY + YEndLine1 - (D / Math.cos(toRadians(alfa)) / 2) - ((c1 + c2) / Math.cos(toRadians(alfa)));

        Lprim = (y1Prim - y2Prim + (x2Prim - x1Prim) * Math.tan(toRadians(alfa))) / (Math.cos(toRadians(alfaw1)) + Math.sin(toRadians(alfaw1)) * Math.tan(toRadians(alfa)));
        LineWithAlfaAndLengthDastak(alfaw1 - 90, StartPointX - XEndLine1 - (w1 * Math.sin(toRadians(alfaw1))) - (fix1 * Math.cos(toRadians(alfaw1))), StartPointY - YEndLine1 - (D / Math.cos(toRadians(alfa)) / 2) + (w1 * Math.cos(toRadians(alfaw1))) - (fix1 * Math.sin(toRadians(alfaw1))), Lprim);
        //////ترسیم خط کوچک بالا دومین خط x
        LineWithAlfaAndLengthDastak(180 + alfaw1, StartPointX - XEndLine1 - (w1 * Math.sin(toRadians(alfaw1))) - (fix1 * Math.cos(toRadians(alfaw1))), StartPointY - YEndLine1 - (D / Math.cos(toRadians(alfa)) / 2) + (w1 * Math.cos(toRadians(alfaw1))) - (fix1 * Math.sin(toRadians(alfaw1))), x);
        /////ترسیم خط سوم
        xLine3 = (StartPointX - XEndLine1 - (w1 * Math.sin(toRadians(alfaw1))) - (fix1 * Math.cos(toRadians(alfaw1)))) - x * Math.cos(toRadians(alfaw1));
        yLine3 = (StartPointY - YEndLine1 - (D / Math.cos(toRadians(alfa)) / 2) + (w1 * Math.cos(toRadians(alfaw1))) - (fix1 * Math.sin(toRadians(alfaw1)))) - x * Math.sin(toRadians(alfaw1));
        LprimLin3 = (yLine3 - y2Prim + (x2Prim - xLine3) * Math.tan(toRadians(alfa))) / (Math.cos(toRadians(alfaw1)) + Math.sin(toRadians(alfaw1)) * Math.tan(toRadians(alfa)));
        LineWithAlfaAndLengthDastak(alfaw1 - 90, xLine3, yLine3, LprimLin3);
        /////////////ترسیم خط hmin/3
        LineWithAlfaAndLengthDastak(180 + alfaw1, xLine3, yLine3, (hminw1 / 3));
        ///////////ترسیم خط چهارم
        xLine4 = (StartPointX - XEndLine1 - (w1 * Math.sin(toRadians(alfaw1))) - (fix1 * Math.cos(toRadians(alfaw1)))) - x * Math.cos(toRadians(alfaw1)) - ((hminw1 / 3)) * Math.cos(toRadians(alfaw1));
        yLine4 = (StartPointY - YEndLine1 - (D / Math.cos(toRadians(alfa)) / 2) + (w1 * Math.cos(toRadians(alfaw1))) - (fix1 * Math.sin(toRadians(alfaw1)))) - x * Math.sin(toRadians(alfaw1)) - ((hminw1 / 3)) * Math.sin(toRadians(alfaw1));
        LprimLin4 = (yLine4 - y2Prim + (x2Prim - xLine4) * Math.tan(toRadians(alfa))) / (Math.cos(toRadians(alfaw1)) + Math.sin(toRadians(alfaw1)) * Math.tan(toRadians(alfa)));
        xLine41 = (StartPointX - XEndLine1 - (w1 * Math.sin(toRadians(alfaw1))) - (fix1 * Math.cos(toRadians(alfaw1)))) - x * Math.cos(toRadians(alfaw1)) - ((h / 3)) * Math.cos(toRadians(alfaw1));
        yLine41 = (StartPointY - YEndLine1 - (D / Math.cos(toRadians(alfa)) / 2) + (w1 * Math.cos(toRadians(alfaw1))) - (fix1 * Math.sin(toRadians(alfaw1)))) - x * Math.sin(toRadians(alfaw1)) - ((h / 3)) * Math.sin(toRadians(alfaw1));
        xPoint1 = (StartPointX - XEndLine1 - (w1 * Math.sin(toRadians(alfaw1))) - (fix1 * Math.cos(toRadians(alfaw1)))) - x * Math.cos(toRadians(alfaw1)) - (((h) / 3)) * Math.cos(toRadians(alfaw1));
        yPoint1 = (StartPointY - YEndLine1 - (D / Math.cos(toRadians(alfa)) / 2) + (w1 * Math.cos(toRadians(alfaw1))) - (fix1 * Math.sin(toRadians(alfaw1)))) - x * Math.sin(toRadians(alfaw1)) - (((h) / 3)) * Math.sin(toRadians(alfaw1));
        LprimLin5 = (yPoint1 - y2Prim + (x2Prim - xPoint1) * Math.tan(toRadians(alfa))) / (Math.cos(toRadians(alfaw1)) + Math.sin(toRadians(alfaw1)) * Math.tan(toRadians(alfa)));
        xPoint2 = xPoint1 + LprimLin5 * Math.sin(toRadians(alfaw1));
        yPoint2 = yPoint1 - LprimLin5 * Math.cos(toRadians(alfaw1));
        DrawLineWithTwoPointDastak(xLine4, yLine4, xPoint2, yPoint2);

        LW1p = calOfTwoPoint(xLine4, yLine4, xPoint2, yPoint2);
        LB1W1 = calOfTwoPoint(xLine4, yLine4, StartPointX - XEndLine1 - (w1 * Math.sin(toRadians(alfaw1))), StartPointY - YEndLine1 - (D / Math.cos(toRadians(alfa)) / 2) + (w1 * Math.cos(toRadians(alfaw1))));
        $('#HDFLW1p').val((parseFloat(LW1p)*parseFloat(ScaleNormal)).toFixed(3));
        $('#HDFLB1W1').val((parseFloat(LB1W1) * parseFloat(ScaleNormal)).toFixed(3));
    }
}

/////سمت چپ پایین
function DrawDastakNew2(h, x, alfa, D, L, w2, b2, c1, c2, j, alfaw2, hminw2, t, StartPointX, StartPointY, Scale, Meyar) {
    h *= Scale; x *= Scale; D *= Scale; L *= Scale; w2 *= Scale;
    b2 *= Scale; c1 *= Scale; c2 *= Scale; hminw2 *= Scale; t *= Scale;
    fix1 = 0.35 * 100; fix1 *= Scale;
    fix2 = 0.25 * 100; fix2 *= Scale;

    ScaleNormal = (1 / Scale);
    h += t;

    XEndLine1 = (L / 2) * Math.cos(toRadians(alfa));
    YEndLine1 = (L / 2) * Math.sin(toRadians(alfa));
    LineWithAlfaAndLengthDastak(270 - alfaw2, StartPointX - XEndLine1, StartPointY + (D / Math.cos(toRadians(alfa)) / 2) - YEndLine1, w2);

    LW2j = w2/100;
    $('#HDFLW2j').val((parseFloat(LW2j) * parseFloat(ScaleNormal)).toFixed(3));

    T = (c1 + c2 + j) / Math.cos(toRadians(alfa));
    Q = fix1 / Math.sin(toRadians(alfaw2));
    if (Q <= T) {
        x1 = 0; y1 = Q;
        LineWithAlfaAndLengthDastak(90, StartPointX - XEndLine1 + (w2 * Math.cos(toRadians(270 - alfaw2))), StartPointY - YEndLine1 + (D / Math.cos(toRadians(alfa)) / 2) + (w2 * Math.sin(toRadians(270 - alfaw2))), Q);

        LineWithAlfaAndLengthDastak(270 - alfaw2, StartPointX - XEndLine1, StartPointY - YEndLine1 + (D / Math.cos(toRadians(alfa)) / 2) + y1, w2);

        xX = StartPointX - XEndLine1 + (w2 * Math.cos(toRadians(270 - alfaw2)));
        yX = StartPointY - YEndLine1 + (D / Math.cos(toRadians(alfa)) / 2) + (w2 * Math.sin(toRadians(270 - alfaw2))) + Q;
        LineWithAlfaAndLengthDastak(-90, xX, yX, x / Math.cos(toRadians(270 - alfaw2)));
        //lineDistanceVPlan(-90, xX, yX, x / Math.sin(toRadians(alfaw1)), 1, ScaleNormal, Meyar, 2)

        LineWithAlfaAndLengthDastak(90 - alfaw2, xX, yX + (x / Math.sin(toRadians(alfaw2))), w2);
        ///////خط دوم کوچک پایین
        //LineWithAlfaAndLengthDastak(-90, StartPointX - XEndLine1, StartPointY - YEndLine1 + (D / 2) + y1, x / Math.cos(toRadians(270 - alfaw2)));

        /////////////خط کوچک بالا hmin
        xXhMin = xX;
        yXhMin = yX - (x / Math.cos(toRadians(270 - alfaw2)));
        LineWithAlfaAndLengthDastak(-90, xXhMin, yXhMin, (hminw2 / 3) / Math.cos(toRadians(270 - alfaw2)));
        //lineDistanceVPlan(-90, xXhMin, yXhMin, (hminw1 / 3) / Math.sin(toRadians(alfaw1)), 1, ScaleNormal, Meyar, 2)

        /////////////خط چهارم
        xsefr1 = (StartPointX - XEndLine1) + (fix1 + x + (h / 3)) * Math.cos(toRadians(180 - alfaw2));
        ysefr1 = (StartPointY - YEndLine1 + (D / Math.cos(toRadians(alfa)) / 2)) + (fix1 + x + (h / 3)) * Math.sin(toRadians(180 - alfaw2));
        xsefr2 = xX;
        ysefr2 = yXhMin - ((hminw2 / 3) / Math.cos(toRadians(270 - alfaw2)));

        Y = ((ysefr2 - ysefr1) / (xsefr2 - xsefr1)) * ((StartPointX - XEndLine1) - xsefr1) + (ysefr1);
        DrawLineWithTwoPointDastak((StartPointX - XEndLine1), Y, xX, ysefr2);
        DrawLineWithTwoPointDastak((StartPointX - XEndLine1), (StartPointY - YEndLine1 + (D / Math.cos(toRadians(alfa)) / 2)), (StartPointX - XEndLine1), Y);

        LW2p = calOfTwoPoint((StartPointX - XEndLine1), Y, xX, ysefr2);
        LB1W2 = calOfTwoPoint(StartPointX - XEndLine1 + (w2 * Math.cos(toRadians(270 - alfaw2))), StartPointY - YEndLine1 + (D / Math.cos(toRadians(alfa)) / 2) + (w2 * Math.sin(toRadians(270 - alfaw2))), xX, ysefr2);
        LB2W2 = calOfTwoPoint(StartPointX - (L / 2) * Math.cos(toRadians(alfa)), StartPointY + (D / Math.cos(toRadians(alfa)) / 2) + (c1 / Math.cos(toRadians(alfa))) + (c2 / Math.cos(toRadians(alfa))) - (L / 2) * Math.sin(toRadians(alfa)), (StartPointX - XEndLine1), Y);
        $('#HDFLW2p').val((parseFloat(LW2p) * parseFloat(ScaleNormal)).toFixed(3));
        $('#HDFLB1W2').val((parseFloat(LB1W2) * parseFloat(ScaleNormal)).toFixed(3));
        $('#HDFLB2W2').val((parseFloat(LB2W2) * parseFloat(ScaleNormal)).toFixed(3));
    }
    else {
        x1 = (-1) * fix1 * Math.cos(toRadians(alfaw2)) + (T - fix1 * Math.sin(toRadians(alfaw2))) * Math.tan(toRadians(alfaw2))
        y1 = T;
        LineWithAlfaAndLengthDastak(180 - alfaw2, StartPointX - XEndLine1 - (w2 * Math.sin(toRadians(180 - alfaw2))), StartPointY - YEndLine1 + (D / Math.cos(toRadians(alfa)) / 2) + (w2 * Math.cos(toRadians(180 - alfaw2))), fix1);

        x1Prim = StartPointX - XEndLine1 - (w2 * Math.sin(toRadians(180 - alfaw2))) - (fix1 * Math.sin(toRadians(90 - alfaw2)));
        y1Prim = StartPointY - YEndLine1 + (D / Math.cos(toRadians(alfa)) / 2) + (w2 * Math.cos(toRadians(180 - alfaw2))) + (fix1 * Math.cos(toRadians(90 - alfaw2)))

        x2Prim = StartPointX + XEndLine1;
        y2Prim = StartPointY + YEndLine1 + (D / Math.cos(toRadians(alfa)) / 2) + ((c1 + c2) / Math.cos(toRadians(alfa)));

        Lprim = (y1Prim - y2Prim + (x2Prim - x1Prim) * Math.tan(toRadians(alfa))) / (Math.cos(toRadians(180 - alfaw2)) + Math.sin(toRadians(180 - alfaw2)) * Math.tan(toRadians(alfa)));
        LineWithAlfaAndLengthDastak(90 - alfaw2, StartPointX - XEndLine1 - (w2 * Math.sin(toRadians(180 - alfaw2))) - (fix1 * Math.sin(toRadians(90 - alfaw2))), StartPointY - YEndLine1 + (D / Math.cos(toRadians(alfa)) / 2) + (w2 * Math.cos(toRadians(180 - alfaw2))) + (fix1 * Math.cos(toRadians(90 - alfaw2))), Lprim);
        ////ترسیم خط کوچک بالا دومین خط x
        LineWithAlfaAndLengthDastak(180 - alfaw2, StartPointX - XEndLine1 - (w2 * Math.sin(toRadians(180 - alfaw2))) - (fix1 * Math.sin(toRadians(90 - alfaw2))), StartPointY - YEndLine1 + (D / Math.cos(toRadians(alfa)) / 2) + (w2 * Math.cos(toRadians(180 - alfaw2))) + (fix1 * Math.cos(toRadians(90 - alfaw2))), x);
    //    /////ترسیم خط سوم
        xLine3 = (StartPointX - XEndLine1 - (w2 * Math.sin(toRadians(180 - alfaw2))) - (fix1 * Math.sin(toRadians(90 - alfaw2)))) - x * Math.sin(toRadians(90 - alfaw2));
        yLine3 = (StartPointY - YEndLine1 + (D / Math.cos(toRadians(alfa)) / 2) + (w2 * Math.cos(toRadians(180 - alfaw2))) + (fix1 * Math.cos(toRadians(90 - alfaw2)))) + x * Math.cos(toRadians(90 - alfaw2));
        LprimLin3 = (yLine3 - y2Prim + (x2Prim - xLine3) * Math.tan(toRadians(alfa))) / (Math.cos(toRadians(180 - alfaw2)) + Math.sin(toRadians(180 - alfaw2)) * Math.tan(toRadians(alfa)));
        LineWithAlfaAndLengthDastak(90 - alfaw2, xLine3, yLine3, LprimLin3);
        /////////////ترسیم خط hmin/3
        LineWithAlfaAndLengthDastak(180 - alfaw2, xLine3, yLine3, (hminw2 / 3));
    //    ///////////ترسیم خط چهارم
        xLine4 = (StartPointX - XEndLine1 - (w2 * Math.sin(toRadians(180 - alfaw2))) - (fix1 * Math.sin(toRadians(90 - alfaw2)))) - x * Math.sin(toRadians(90 - alfaw2)) - ((hminw2 / 3)) * Math.sin(toRadians(90 - alfaw2));
        yLine4 = (StartPointY - YEndLine1 + (D / Math.cos(toRadians(alfa)) / 2) + (w2 * Math.cos(toRadians(180 - alfaw2))) + (fix1 * Math.cos(toRadians(90 - alfaw2)))) + x * Math.cos(toRadians(90 - alfaw2)) + ((hminw2 / 3)) * Math.cos(toRadians(90 - alfaw2));
        LprimLin4 = (yLine4 - y2Prim + (x2Prim - xLine4) * Math.tan(toRadians(alfa))) / (Math.cos(toRadians(alfaw2)) + Math.sin(toRadians(alfaw2)) * Math.tan(toRadians(alfa)));

        xLine41 = (StartPointX - XEndLine1 - (w2 * Math.sin(toRadians(180 - alfaw2))) - (fix1 * Math.sin(toRadians(90 - alfaw2)))) - x * Math.sin(toRadians(90 - alfaw2)) - ((h / 3)) * Math.sin(toRadians(90 - alfaw2));
        yLine41 = (StartPointY - YEndLine1 + (D / Math.cos(toRadians(alfa)) / 2) + (w2 * Math.cos(toRadians(180 - alfaw2))) + (fix1 * Math.cos(toRadians(90 - alfaw2)))) + x * Math.cos(toRadians(90 - alfaw2)) + ((h / 3)) * Math.cos(toRadians(90 - alfaw2));

        xPoint1 = (StartPointX - XEndLine1 - (w2 * Math.sin(toRadians(180 - alfaw2))) - (fix1 * Math.sin(toRadians(90 - alfaw2)))) - x * Math.sin(toRadians(90 - alfaw2)) - (((h) / 3)) * Math.sin(toRadians(90 - alfaw2));
        yPoint1 = (StartPointY - YEndLine1 + (D / Math.cos(toRadians(alfa)) / 2) + (w2 * Math.cos(toRadians(180 - alfaw2))) + (fix1 * Math.cos(toRadians(90 - alfaw2)))) + x * Math.cos(toRadians(90 - alfaw2)) + (((h) / 3)) * Math.cos(toRadians(90 - alfaw2));

        LprimLin5 = (yPoint1 - y2Prim + (x2Prim - xPoint1) * Math.tan(toRadians(alfa))) / (Math.cos(toRadians(180 - alfaw2)) + Math.sin(toRadians(180 - alfaw2)) * Math.tan(toRadians(alfa)));
        xPoint2 = xPoint1 + LprimLin5 * Math.sin(toRadians(180 - alfaw2));
        yPoint2 = yPoint1 - LprimLin5 * Math.cos(toRadians(180 - alfaw2));
        DrawLineWithTwoPointDastak(xLine4, yLine4, xPoint2, yPoint2);

        LW2p = calOfTwoPoint(xLine4, yLine4, xPoint2, yPoint2);
        LB1W2 = calOfTwoPoint(xLine4, yLine4, StartPointX - XEndLine1 - (w2 * Math.sin(toRadians(180 - alfaw2))), StartPointY - YEndLine1 + (D / Math.cos(toRadians(alfa)) / 2) + (w2 * Math.cos(toRadians(180 - alfaw2))));

        $('#HDFLW2p').val((parseFloat(LW2p) * parseFloat(ScaleNormal)).toFixed(3));
        $('#HDFLB1W2').val((parseFloat(LB1W2) * parseFloat(ScaleNormal)).toFixed(3));
    }
}
/////////سمت راست بالا
function DrawDastakNew3(h, x, alfa, D, L, w3, b2, c1, c2, j, alfaw3, hminw3, t, StartPointX, StartPointY, Scale, Meyar) {
    h *= Scale; x *= Scale; D *= Scale; L *= Scale; w3 *= Scale;
    b2 *= Scale; c1 *= Scale; c2 *= Scale; hminw3 *= Scale; t *= Scale;
    fix1 = 0.35 * 100; fix1 *= Scale;
    fix2 = 0.25 * 100; fix2 *= Scale;
    XEndLine1 = (L / 2) * Math.cos(toRadians(alfa));
    YEndLine1 = (L / 2) * Math.sin(toRadians(alfa));

    ScaleNormal = (1 / Scale);
    LW3j = w3/100;
    $('#HDFLW3j').val((parseFloat(LW3j) * parseFloat(ScaleNormal)).toFixed(3));

    h += t;

    LineWithAlfaAndLengthDastak(90 - alfaw3, StartPointX + XEndLine1, StartPointY + YEndLine1 - (D / Math.cos(toRadians(alfa)) / 2), w3);

    //lineDistanceHPlan(90 + alfaw1, StartPointX - XEndLine1, StartPointY - YEndLine1 - (D / 2), w1, 10, 2, ScaleNormal, Meyar, 2)

    T = (c1 + c2 + j) / Math.cos(toRadians(alfa));
    Q = fix1 / Math.sin(toRadians(alfaw3));
    if (Q <= T) {
        x1 = 0; y1 = Q;
        LineWithAlfaAndLengthDastak(-90, StartPointX + XEndLine1 + (w3 * Math.sin(toRadians(alfaw3))), StartPointY + YEndLine1 - (D / Math.cos(toRadians(alfa)) / 2) + (w3 * Math.cos(toRadians(alfaw3))), Q);
        //lineDistanceVPlan(-90, StartPointX - XEndLine1 - (w1 * Math.sin(toRadians(alfaw1))), StartPointY - YEndLine1 - (D / 2) + (w1 * Math.cos(toRadians(alfaw1))), Q, 1, ScaleNormal, Meyar, 2)

        LineWithAlfaAndLengthDastak(90 - alfaw3, StartPointX + XEndLine1, StartPointY + YEndLine1 - (D / Math.cos(toRadians(alfa)) / 2) - y1, w3);
        //LineWithAlfaAndLengthDastak(-90, StartPointX + XEndLine1, StartPointY + YEndLine1 - (D / 2), Q);

        xX = StartPointX + XEndLine1 + (w3 * Math.sin(toRadians(alfaw3)));
        yX = StartPointY + YEndLine1 - (D / Math.cos(toRadians(alfa)) / 2) + (w3 * Math.cos(toRadians(alfaw3))) - Q;
        LineWithAlfaAndLengthDastak(-90, xX, yX, x / Math.sin(toRadians(alfaw3)));
        //lineDistanceVPlan(-90, xX, yX, x / Math.sin(toRadians(alfaw3)), 1, ScaleNormal, Meyar, 2)

        LineWithAlfaAndLengthDastak(270 - alfaw3, xX, yX - (x / Math.sin(toRadians(alfaw3))), w3);
        /////خط دوم کوچک پایین
        LineWithAlfaAndLengthDastak(-90, StartPointX + XEndLine1, StartPointY + YEndLine1 - (D / Math.cos(toRadians(alfa)) / 2) - y1, x / Math.sin(toRadians(alfaw3)));

        ///////////خط کوچک بالا hmin
        xXhMin = xX;
        yXhMin = yX - (x / Math.sin(toRadians(alfaw3)));
        LineWithAlfaAndLengthDastak(-90, xXhMin, yXhMin, (hminw3 / 3) / Math.sin(toRadians(alfaw3)));
        ///////////خط چهارم
        xsefr1 = (StartPointX + XEndLine1) + (fix1 + x + (h / 3)) * Math.cos(toRadians(- alfaw3));
        ysefr1 = (StartPointY + YEndLine1 - (D / Math.cos(toRadians(alfa)) / 2)) + (fix1 + x + (h / 3)) * Math.sin(toRadians(-alfaw3));

        xsefr2 = xX;
        ysefr2 = yXhMin - ((hminw3 / 3) / Math.sin(toRadians(alfaw3)));

        Y = ((ysefr2 - ysefr1) / (xsefr2 - xsefr1)) * ((StartPointX + XEndLine1) - xsefr1) + (ysefr1);
        DrawLineWithTwoPointDastak((StartPointX + XEndLine1), Y, xX, ysefr2);
        DrawLineWithTwoPointDastak((StartPointX + XEndLine1), (StartPointY + YEndLine1 - (D / Math.cos(toRadians(alfa)) / 2)), (StartPointX + XEndLine1), Y);

        LW3p = calOfTwoPoint((StartPointX + XEndLine1), Y, xX, ysefr2);
        LB1W3 = calOfTwoPoint(StartPointX + XEndLine1 + (w3 * Math.sin(toRadians(alfaw3))), StartPointY + YEndLine1 - (D / Math.cos(toRadians(alfa)) / 2) + (w3 * Math.cos(toRadians(alfaw3))), xX, ysefr2);
        LB2W3 = calOfTwoPoint(StartPointX + XEndLine1, StartPointY + YEndLine1 - (D / Math.cos(toRadians(alfa)) / 2) - ((c1 + c2) / Math.cos(toRadians(alfa))), (StartPointX + XEndLine1), Y);

        $('#HDFLW3p').val((parseFloat(LW3p) * parseFloat(ScaleNormal)).toFixed(3));
        $('#HDFLB1W3').val((parseFloat(LB1W3) * parseFloat(ScaleNormal)).toFixed(3));
        $('#HDFLB2W3').val((parseFloat(LB2W3) * parseFloat(ScaleNormal)).toFixed(3));
    }
    else {
        x1 = (-1) * fix1 * Math.cos(toRadians(alfaw3)) + (T - fix1 * Math.sin(toRadians(alfaw3))) * Math.tan(toRadians(alfaw3))
        y1 = T;

        LineWithAlfaAndLengthDastak(-alfaw3, StartPointX + XEndLine1 - (w3 * Math.sin(toRadians(-alfaw3))), StartPointY + YEndLine1 - (D / Math.cos(toRadians(alfa)) / 2) + (w3 * Math.cos(toRadians(-alfaw3))), fix1);

        x1Prim = StartPointX + XEndLine1 - (w3 * Math.sin(toRadians(-alfaw3))) + (fix1 * Math.cos(toRadians(-alfaw3)));
        y1Prim = StartPointY + YEndLine1 - (D / Math.cos(toRadians(alfa)) / 2) + (w3 * Math.cos(toRadians(-alfaw3))) + (fix1 * Math.sin(toRadians(-alfaw3)))


        x2Prim = StartPointX + XEndLine1;
        y2Prim = StartPointY + YEndLine1 - (D / Math.cos(toRadians(alfa)) / 2) - ((c1 + c2) / Math.cos(toRadians(alfa)));

        Lprim = (y1Prim - y2Prim + (x2Prim - x1Prim) * Math.tan(toRadians(alfa))) / (Math.cos(toRadians(-alfaw3)) + Math.sin(toRadians(-alfaw3)) * Math.tan(toRadians(alfa)));
        LineWithAlfaAndLengthDastak(270 - alfaw3, StartPointX + XEndLine1 - (w3 * Math.sin(toRadians(-alfaw3))) + (fix1 * Math.cos(toRadians(-alfaw3))),
            StartPointY + YEndLine1 - (D / Math.cos(toRadians(alfa)) / 2) + (w3 * Math.cos(toRadians(-alfaw3))) + (fix1 * Math.sin(toRadians(-alfaw3))), Lprim);
        //////ترسیم خط کوچک بالا دومین خط x
        LineWithAlfaAndLengthDastak(-alfaw3, StartPointX + XEndLine1 - (w3 * Math.sin(toRadians(-alfaw3))) + (fix1 * Math.cos(toRadians(-alfaw3))), StartPointY + YEndLine1 - (D / Math.cos(toRadians(alfa)) / 2) + (w3 * Math.cos(toRadians(-alfaw3))) + (fix1 * Math.sin(toRadians(-alfaw3))), x);
        ///////ترسیم خط سوم
        xLine3 = (StartPointX + XEndLine1 - (w3 * Math.sin(toRadians(-alfaw3))) + (fix1 * Math.cos(toRadians(-alfaw3)))) + x * Math.cos(toRadians(-alfaw3));
        yLine3 = (StartPointY + YEndLine1 - (D / Math.cos(toRadians(alfa)) / 2) + (w3 * Math.cos(toRadians(-alfaw3))) + (fix1 * Math.sin(toRadians(-alfaw3)))) + x * Math.sin(toRadians(-alfaw3));
        LprimLin3 = (yLine3 - y2Prim + (x2Prim - xLine3) * Math.tan(toRadians(alfa))) / (Math.cos(toRadians(-alfaw3)) + Math.sin(toRadians(-alfaw3)) * Math.tan(toRadians(alfa)));
        LineWithAlfaAndLengthDastak(270 - alfaw3, xLine3, yLine3, LprimLin3);
        /////////////ترسیم خط hmin/3
        LineWithAlfaAndLengthDastak(-alfaw3, xLine3, yLine3, (hminw3 / 3));
        /////////////ترسیم خط چهارم
        xLine4 = (StartPointX + XEndLine1 - (w3 * Math.sin(toRadians(-alfaw3))) + (fix1 * Math.cos(toRadians(-alfaw3)))) + x * Math.cos(toRadians(-alfaw3)) + ((hminw3 / 3)) * Math.cos(toRadians(-alfaw3));
        yLine4 = (StartPointY + YEndLine1 - (D / Math.cos(toRadians(alfa)) / 2) + (w3 * Math.cos(toRadians(-alfaw3))) + (fix1 * Math.sin(toRadians(-alfaw3)))) + x * Math.sin(toRadians(-alfaw3)) + ((hminw3 / 3)) * Math.sin(toRadians(-alfaw3));
        LprimLin4 = (yLine4 - y2Prim + (x2Prim - xLine4) * Math.tan(toRadians(alfa))) / (Math.cos(toRadians(-alfaw3)) + Math.sin(toRadians(-alfaw3)) * Math.tan(toRadians(alfa)));

        xLine41 = (StartPointX + XEndLine1 - (w3 * Math.sin(toRadians(-alfaw3))) + (fix1 * Math.cos(toRadians(-alfaw3)))) + x * Math.cos(toRadians(-alfaw3)) + ((h / 3)) * Math.cos(toRadians(-alfaw3));
        yLine41 = (StartPointY + YEndLine1 - (D / Math.cos(toRadians(alfa)) / 2) + (w3 * Math.cos(toRadians(-alfaw3))) + (fix1 * Math.sin(toRadians(-alfaw3)))) + x * Math.sin(toRadians(-alfaw3)) + ((h / 3)) * Math.sin(toRadians(-alfaw3));

        xPoint1 = (StartPointX + XEndLine1 - (w3 * Math.sin(toRadians(-alfaw3))) + (fix1 * Math.cos(toRadians(-alfaw3)))) + x * Math.cos(toRadians(-alfaw3)) + (((h) / 3)) * Math.cos(toRadians(-alfaw3));
        yPoint1 = (StartPointY + YEndLine1 - (D / Math.cos(toRadians(alfa)) / 2) + (w3 * Math.cos(toRadians(-alfaw3))) + (fix1 * Math.sin(toRadians(-alfaw3)))) + x * Math.sin(toRadians(-alfaw3)) + (((h) / 3)) * Math.sin(toRadians(-alfaw3));
        LprimLin5 = (yPoint1 - y2Prim + (x2Prim - xPoint1) * Math.tan(toRadians(alfa))) / (Math.cos(toRadians(-alfaw3)) + Math.sin(toRadians(-alfaw3)) * Math.tan(toRadians(alfa)));

        xPoint2 = xPoint1 + LprimLin5 * Math.sin(toRadians(-alfaw3));
        yPoint2 = yPoint1 - LprimLin5 * Math.cos(toRadians(-alfaw3));
        DrawLineWithTwoPointDastak(xLine4, yLine4, xPoint2, yPoint2);

        LW3p = calOfTwoPoint(xLine4, yLine4, xPoint2, yPoint2);
        LB1W3 = calOfTwoPoint(xLine4, yLine4, StartPointX + XEndLine1 - (w3 * Math.sin(toRadians(-alfaw3))), StartPointY + YEndLine1 - (D / Math.cos(toRadians(alfa)) / 2) + (w3 * Math.cos(toRadians(-alfaw3))));

        $('#HDFLW3p').val((parseFloat(LW3p) * parseFloat(ScaleNormal)).toFixed(3));
        $('#HDFLB1W3').val((parseFloat(LB1W3) * parseFloat(ScaleNormal)).toFixed(3));
    }
}

/////سمت راست پایین
function DrawDastakNew4(h, x, alfa, D, L, w4, b2, c1, c2, j, alfaw4, hminw4, t, StartPointX, StartPointY, Scale, Meyar) {
    h *= Scale; x *= Scale; D *= Scale; L *= Scale; w4 *= Scale;
    b2 *= Scale; c1 *= Scale; c2 *= Scale; hminw4 *= Scale; t *= Scale;
    fix1 = 0.35 * 100; fix1 *= Scale;
    fix2 = 0.25 * 100; fix2 *= Scale;
    XEndLine1 = (L / 2) * Math.cos(toRadians(alfa));
    YEndLine1 = (L / 2) * Math.sin(toRadians(alfa));
    LineWithAlfaAndLengthDastak(alfaw4 - 90, StartPointX + XEndLine1, StartPointY + (D / Math.cos(toRadians(alfa)) / 2) + YEndLine1, w4);

    ScaleNormal = (1 / Scale);
    LW4j = w4/100;
    $('#HDFLW4j').val((parseFloat(LW4j) * parseFloat(ScaleNormal)).toFixed(3));

    h += t;

    T = (c1 + c2 + j) / Math.cos(toRadians(alfa));
    Q = fix1 / Math.sin(toRadians(alfaw4));
    if (Q <= T) {
        x1 = 0; y1 = Q;
        LineWithAlfaAndLengthDastak(90, StartPointX + XEndLine1 + (w4 * Math.cos(toRadians(alfaw4 - 90))), StartPointY + YEndLine1 + (D / Math.cos(toRadians(alfa)) / 2) + (w4 * Math.sin(toRadians(alfaw4 - 90))), Q);

        //lineDistanceVPlan(-90, StartPointX - XEndLine1 - (w1 * Math.sin(toRadians(alfaw1))), StartPointY - YEndLine1 - (D / 2) + (w1 * Math.cos(toRadians(alfaw1))), Q, 1, ScaleNormal, Meyar, 2)

        LineWithAlfaAndLengthDastak(alfaw4 - 90, StartPointX + XEndLine1, StartPointY + (D / Math.cos(toRadians(alfa)) / 2) + YEndLine1 + y1, w4);
        //LineWithAlfaAndLengthDastak(90, StartPointX - XEndLine1 + (w2 * Math.cos(toRadians(270 - alfaw2))), StartPointY - YEndLine1 + (D / 2) + (w2 * Math.sin(toRadians(270 - alfaw2))) + y1, Q);

        xX = StartPointX + XEndLine1 + (w4 * Math.cos(toRadians(alfaw4 - 90)));
        yX = StartPointY + YEndLine1 + (D / Math.cos(toRadians(alfa)) / 2) + (w4 * Math.sin(toRadians(alfaw4 - 90))) + Q;
        LineWithAlfaAndLengthDastak(90, xX, yX, x / Math.cos(toRadians(alfaw4 - 90)));
        //lineDistanceVPlan(-90, xX, yX, x / Math.sin(toRadians(alfaw1)), 1, ScaleNormal, Meyar, 2)

        LineWithAlfaAndLengthDastak(alfaw4 - 270, xX, yX + (x / Math.cos(toRadians(alfaw4 - 90))), w4);
        ///////خط دوم کوچک پایین
        LineWithAlfaAndLengthDastak(90, StartPointX + XEndLine1, StartPointY + YEndLine1 + (D / Math.cos(toRadians(alfa)) / 2) + y1, x / Math.cos(toRadians(alfaw4 - 90)));

        /////////////خط کوچک بالا hmin
        xXhMin = xX;
        yXhMin = yX + (x / Math.cos(toRadians(alfaw4 - 90)));
        LineWithAlfaAndLengthDastak(90, xXhMin, yXhMin, (hminw4 / 3) / Math.cos(toRadians(alfaw4 - 90)));
        //lineDistanceVPlan(-90, xXhMin, yXhMin, (hminw1 / 3) / Math.sin(toRadians(alfaw1)), 1, ScaleNormal, Meyar, 2)

        /////////////خط چهارم
        xsefr1 = (StartPointX + XEndLine1) - (fix1 + x + (h / 3)) * Math.sin(toRadians(alfaw4 - 90));
        ysefr1 = (StartPointY + YEndLine1 + (D / Math.cos(toRadians(alfa)) / 2)) + (fix1 + x + (h / 3)) * Math.cos(toRadians(alfaw4 - 90));
        xsefr2 = xX;
        ysefr2 = yXhMin + ((hminw4 / 3) / Math.cos(toRadians(alfaw4 - 90)));

        Y = ((ysefr2 - ysefr1) / (xsefr2 - xsefr1)) * ((StartPointX + XEndLine1) - xsefr1) + (ysefr1);
        DrawLineWithTwoPointDastak((StartPointX + XEndLine1), Y, xX, ysefr2);
        DrawLineWithTwoPointDastak((StartPointX + XEndLine1), (StartPointY + YEndLine1 + (D / Math.cos(toRadians(alfa)) / 2)), (StartPointX + XEndLine1), Y);

        LW4p = calOfTwoPoint((StartPointX + XEndLine1), Y, xX, ysefr2);
        LB1W4 = calOfTwoPoint(StartPointX + XEndLine1 + (w4 * Math.cos(toRadians(alfaw4 - 90))), StartPointY + YEndLine1 + (D / Math.cos(toRadians(alfa)) / 2) + (w4 * Math.sin(toRadians(alfaw4 - 90))), xX, ysefr2);
        LB2W4 = calOfTwoPoint(StartPointX + (L / 2) * Math.cos(toRadians(alfa)), StartPointY + (D / Math.cos(toRadians(alfa)) / 2) + (c1 / Math.cos(toRadians(alfa))) + (c2 / Math.cos(toRadians(alfa))) + (L / 2) * Math.sin(toRadians(alfa)), (StartPointX + XEndLine1), Y);

        $('#HDFLW4p').val((parseFloat(LW4p) * parseFloat(ScaleNormal)).toFixed(3));
        $('#HDFLB1W4').val((parseFloat(LB1W4) * parseFloat(ScaleNormal)).toFixed(3));
        $('#HDFLB2W4').val((parseFloat(LB2W4) * parseFloat(ScaleNormal)).toFixed(3));
    }
    else {
        x1 = (-1) * fix1 * Math.cos(toRadians(alfaw1)) + (T - fix1 * Math.sin(toRadians(alfaw1))) * Math.tan(toRadians(alfaw1))
        y1 = T;

        LineWithAlfaAndLengthDastak(alfaw4 - 90, StartPointX + XEndLine1, StartPointY + (D / Math.cos(toRadians(alfa)) / 2) + YEndLine1, w4);
        LineWithAlfaAndLengthDastak(alfaw4, StartPointX + XEndLine1 + (w4 * Math.cos(toRadians(alfaw4 - 90))), StartPointY + YEndLine1 + (D / Math.cos(toRadians(alfa)) / 2) + (w4 * Math.sin(toRadians(alfaw4 - 90))), fix1);

        x1Prim = StartPointX + XEndLine1 + (w4 * Math.cos(toRadians(alfaw4 - 90))) + (fix1 * Math.cos(toRadians(alfaw4)));
        y1Prim = StartPointY + YEndLine1 + (D / Math.cos(toRadians(alfa)) / 2) + (w4 * Math.sin(toRadians(alfaw4 - 90))) + (fix1 * Math.sin(toRadians(alfaw4)))

        x2Prim = StartPointX + XEndLine1;
        y2Prim = StartPointY + YEndLine1 + (D / Math.cos(toRadians(alfa)) / 2) + ((c1 + c2) / Math.cos(toRadians(alfa)));

        Lprim = (y1Prim - y2Prim + (x2Prim - x1Prim) * Math.tan(toRadians(alfa))) / (Math.cos(toRadians(alfaw4)) + Math.sin(toRadians(alfaw4)) * Math.tan(toRadians(alfa)));
        LineWithAlfaAndLengthDastak(alfaw4 + 270, StartPointX + XEndLine1 + (w4 * Math.cos(toRadians(alfaw4 - 90))) + (fix1 * Math.cos(toRadians(alfaw4))),
            StartPointY + YEndLine1 + (D / Math.cos(toRadians(alfa)) / 2) + (w4 * Math.sin(toRadians(alfaw4 - 90))) + (fix1 * Math.sin(toRadians(alfaw4))), Lprim);
        ////ترسیم خط کوچک بالا دومین خط x
        LineWithAlfaAndLengthDastak(alfaw4, StartPointX + XEndLine1 + (w4 * Math.cos(toRadians(alfaw4 - 90))) + (fix1 * Math.cos(toRadians(alfaw4))),
            StartPointY + YEndLine1 + (D / Math.cos(toRadians(alfa)) / 2) + (w4 * Math.sin(toRadians(alfaw4 - 90))) + (fix1 * Math.sin(toRadians(alfaw4))), x);
        ///////ترسیم خط سوم
        xLine3 = (StartPointX + XEndLine1 + (w4 * Math.cos(toRadians(alfaw4 - 90))) + (fix1 * Math.cos(toRadians(alfaw4)))) + x * Math.cos(toRadians(alfaw4));
        yLine3 = (StartPointY + YEndLine1 + (D / Math.cos(toRadians(alfa)) / 2) + (w4 * Math.sin(toRadians(alfaw4 - 90))) + (fix1 * Math.sin(toRadians(alfaw4)))) + x * Math.sin(toRadians(alfaw4));
        LprimLin3 = (yLine3 - y2Prim + (x2Prim - xLine3) * Math.tan(toRadians(alfa))) / (Math.cos(toRadians(alfaw4)) + Math.sin(toRadians(alfaw4)) * Math.tan(toRadians(alfa)));
        LineWithAlfaAndLengthDastak(alfaw4 + 270, xLine3, yLine3, LprimLin3);
        /////////////ترسیم خط hmin/3
        LineWithAlfaAndLengthDastak(alfaw4, xLine3, yLine3, (hminw4 / 3));
        /////////////ترسیم خط چهارم
        xLine4 = (StartPointX + XEndLine1 + (w4 * Math.cos(toRadians(alfaw4 - 90))) + (fix1 * Math.cos(toRadians(alfaw4)))) + x * Math.cos(toRadians(alfaw4)) + ((hminw4 / 3)) * Math.cos(toRadians(alfaw4));
        yLine4 = (StartPointY + YEndLine1 + (D / Math.cos(toRadians(alfa)) / 2) + (w4 * Math.sin(toRadians(alfaw4 - 90))) + (fix1 * Math.sin(toRadians(alfaw4)))) + x * Math.sin(toRadians(alfaw4)) + ((hminw4 / 3)) * Math.sin(toRadians(alfaw4));
        LprimLin4 = (yLine4 - y2Prim + (x2Prim - xLine4) * Math.tan(toRadians(alfa))) / (Math.cos(toRadians(alfaw4)) + Math.sin(toRadians(alfaw4)) * Math.tan(toRadians(alfa)));

        xPoint1 = (StartPointX + XEndLine1 + (w4 * Math.cos(toRadians(alfaw4-90))) + (fix1 * Math.cos(toRadians(alfaw4)))) + x * Math.cos(toRadians(alfaw4)) + (((h) / 3)) * Math.cos(toRadians(alfaw4));
        yPoint1 = (StartPointY + YEndLine1 + (D / Math.cos(toRadians(alfa)) / 2) + (w4 * Math.sin(toRadians(alfaw4 - 90))) + (fix1 * Math.sin(toRadians(alfaw4)))) + x * Math.sin(toRadians(alfaw4)) + (((h) / 3)) * Math.sin(toRadians(alfaw4));
        LprimLin5 = (yPoint1 - y2Prim + (x2Prim - xPoint1) * Math.tan(toRadians(alfa))) / (Math.cos(toRadians(alfaw4)) + Math.sin(toRadians(alfaw4)) * Math.tan(toRadians(alfa)));
        xPoint2 = xPoint1 + LprimLin5 * Math.sin(toRadians(alfaw4));
        yPoint2 = yPoint1 - LprimLin5 * Math.cos(toRadians(alfaw4));
        DrawLineWithTwoPointDastak(xLine4, yLine4, xPoint2, yPoint2);

        LW4p = calOfTwoPoint(xLine4, yLine4, xPoint2, yPoint2);
        LB1W4 = calOfTwoPoint(xLine4, yLine4, StartPointX + XEndLine1 + (w4 * Math.cos(toRadians(alfaw4 - 90))), StartPointY + YEndLine1 + (D / Math.cos(toRadians(alfa)) / 2) + (w4 * Math.sin(toRadians(alfaw4 - 90))));

        $('#HDFLW4p').val((parseFloat(LW4p) * parseFloat(ScaleNormal)).toFixed(3));
        $('#HDFLB1W4').val((parseFloat(LB1W4) * parseFloat(ScaleNormal)).toFixed(3));
    }
}

function DrawLineWithTwoPointDastak(x1, y1, x2, y2) {
    debugger;
    Line = "<line x1=\"" + x1 + "\" x2=\"" + x2 + "\" y1=\"" + y1 + "\" y2=\"" + y2 + "\" style=\"stroke: rgb(52, 101, 18); stroke-width:1\" />";
    $('#AbnieShow').find('#svgLinesPlan').append(Line);
    $('#AbnieShow').find('#svgContainerPlan').html($('#AbnieShow').find('#svgContainerPlan').html());
}

function LineWithAlfaAndLength(theta, x, y, Len) {
    debugger;

    xnew = x + Len * Math.cos(toRadians(theta));
    ynew = y + Len * Math.sin(toRadians(theta))
    Line = "<line x1=\"" + x + "\" x2=\"" + xnew + "\" y1=\"" + y + "\" y2=\"" + ynew + "\" style=\"stroke:rgb(31, 0, 116); stroke-width:1\" />";
    $('#AbnieShow').find('#svgLinesPlan').append(Line);
    $('#AbnieShow').find('#svgContainerPlan').html($('#AbnieShow').find('#svgContainerPlan').html());
}

function LineWithAlfaAndLengthDash(theta, x, y, Len) {
    debugger;

    xnew = x + Len * Math.cos(toRadians(theta));
    ynew = y + Len * Math.sin(toRadians(theta))
    Line = "<line x1=\"" + x + "\" x2=\"" + xnew + "\" y1=\"" + y + "\" y2=\"" + ynew + "\" stroke-dasharray=\"4\" style=\"stroke:rgb(31, 0, 116); stroke-width:1\" />";
    $('#AbnieShow').find('#svgLinesPlan').append(Line);
    $('#AbnieShow').find('#svgContainerPlan').html($('#AbnieShow').find('#svgContainerPlan').html());
}

function LineWithAlfaAndLengthDastak(theta, x, y, Len) {
    debugger;

    xnew = x + Len * Math.cos(toRadians(theta));
    ynew = y + Len * Math.sin(toRadians(theta))
    Line = "<line x1=\"" + x + "\" x2=\"" + xnew + "\" y1=\"" + y + "\" y2=\"" + ynew + "\" style=\"stroke: rgb(52, 101, 18); stroke-width:1\" />";
    $('#AbnieShow').find('#svgLinesPlan').append(Line);
    $('#AbnieShow').find('#svgContainerPlan').html($('#AbnieShow').find('#svgContainerPlan').html());
}


function toRadians(angle) {
    return angle * (Math.PI / 180);
}

function lineDistanceHPlan(theta, x, y, Len, dOfLine, Pos, ScaleNormal, cm) {
    debugger;

    if (Len != 0) {
       xnew = x + Len * Math.cos(toRadians(theta));
       ynew = y + Len * Math.sin(toRadians(theta))

        if (Pos == 1) /////1= L to R
            ////////2= R to L
            Text = "<text style=\"font-size:16px\" x=\"" + (x + ((Len / 2) + 8)) + "\" y=\"" + ((y + dOfLine) - 5) + "\" fill=\"blue\">" + Math.ceil(Len * ScaleNormal) / cm + "</text>";
        else
            Text = "<text style=\"font-size:16px\" x=\"" + (xnew + ((Len / 2) + 8)) + "\" y=\"" + ((ynew + dOfLine) - 5) + "\" fill=\"blue\">" + Math.ceil(Len * ScaleNormal) / cm + "</text>";

        Line = "<line x1=\"" + x + "\" x2=\"" + xnew + "\" y1=\"" + (y + dOfLine) + "\" y2=\"" + (ynew + dOfLine) + "\" style=\"stroke: rgb(38, 130, 161); stroke-width:0.5\" />";

        $('#AbnieShow').find('#svgLinesPlan').append(Line);
        $('#AbnieShow').find('#svgLinesPlan').append(Text);
        Line1 = "<line x1=\"" + (x - 2) + "\" x2=\"" + (x + 2) + "\" y1=\"" + ((y + dOfLine) + 2) + "\" y2=\"" + ((y + dOfLine) - 2) + "\" style=\"stroke: rgb(38, 130, 161); stroke-width:0.5\" />";
        $('#AbnieShow').find('#svgLinesPlan').append(Line1);
        Line1 = "<line x1=\"" + (xnew - 2) + "\" x2=\"" + (xnew + 2) + "\" y1=\"" + ((ynew + dOfLine) + 2) + "\" y2=\"" + ((ynew + dOfLine) - 2) + "\" style=\"stroke: rgb(38, 130, 161); stroke-width:0.5\" />";
        $('#AbnieShow').find('#svgLinesPlan').append(Line1);
        $('#AbnieShow').find('#svgContainerPlan').html($('#AbnieShow').find('#svgContainerPlan').html());
    }
}

function lineDistanceVPlan(theta, x, y, Len, Pos, ScaleNormal, cm, LF) {
    debugger;

    if (Len != 0) {
        xnew = x + Len * Math.cos(toRadians(theta));
        ynew = y + Len * Math.sin(toRadians(theta))
        if (LF == 1) {
            if (Pos == 1) /////1= L to R
                ////////2= R to L
                Text = "<text style=\"font-size:16px\" x=\"" + (x + 45) + "\" y=\"" + (y - (Len / 2)) + "\" fill=\"blue\">" + ((Len * ScaleNormal) / cm).toFixed(2) + "</text>";
            else
                Text = "<text style=\"font-size:16px\" x=\"" + (xnew + 45) + "\" y=\"" + (ynew - (Len / 2)) + "\" fill=\"blue\">" + ((Len * ScaleNormal) / cm).toFixed(2) + "</text>";
        }
        else
        {
            if (Pos == 1) /////1= L to R
                ////////2= R to L
                Text = "<text style=\"font-size:16px\" x=\"" + (x - 15) + "\" y=\"" + (y - (Len / 2)) + "\" fill=\"blue\">" + ((Len * ScaleNormal) / cm).toFixed(2) + "</text>";
            else
                Text = "<text style=\"font-size:16px\" x=\"" + (xnew - 15) + "\" y=\"" + (ynew - (Len / 2)) + "\" fill=\"blue\">" + ((Len * ScaleNormal) / cm).toFixed(2) + "</text>";
        }

        if (LF==1) 
            Line = "<line x1=\"" + (x + 15) + "\" x2=\"" + (xnew + 15) + "\" y1=\"" + y + "\" y2=\"" + ynew + "\" style=\"stroke: rgb(38, 130, 161); stroke-width:0.5\" />";
        else
            Line = "<line x1=\"" + (x - 15) + "\" x2=\"" + (xnew - 15) + "\" y1=\"" + y + "\" y2=\"" + ynew + "\" style=\"stroke: rgb(38, 130, 161); stroke-width:0.5\" />";

        $('#AbnieShow').find('#svgLinesPlan').append(Line);
        $('#AbnieShow').find('#svgLinesPlan').append(Text);
        if (LF == 1)
            Line1 = "<line x1=\"" + ((x + 15) - 2) + "\" x2=\"" + ((x + 15) + 2) + "\" y1=\"" + (y + 2) + "\" y2=\"" + (y - 2) + "\" style=\"stroke: rgb(38, 130, 161); stroke-width:0.5\" />";
        else
            Line1 = "<line x1=\"" + ((x - 15) - 2) + "\" x2=\"" + ((x - 15) + 2) + "\" y1=\"" + (y + 2) + "\" y2=\"" + (y - 2) + "\" style=\"stroke: rgb(38, 130, 161); stroke-width:0.5\" />";


        $('#AbnieShow').find('#svgLinesPlan').append(Line1);
        if (LF == 1)
            Line1 = "<line x1=\"" + ((xnew + 15) - 2) + "\" x2=\"" + ((xnew + 15) + 2) + "\" y1=\"" + (ynew + 2) + "\" y2=\"" + (ynew - 2) + "\" style=\"stroke: rgb(38, 130, 161); stroke-width:0.5\" />";
        else
            Line1 = "<line x1=\"" + ((xnew - 15) - 2) + "\" x2=\"" + ((xnew - 15) + 2) + "\" y1=\"" + (ynew + 2) + "\" y2=\"" + (ynew - 2) + "\" style=\"stroke: rgb(38, 130, 161); stroke-width:0.5\" />";

        $('#AbnieShow').find('#svgLinesPlan').append(Line1);
        $('#AbnieShow').find('#svgContainerPlan').html($('#AbnieShow').find('#svgContainerPlan').html());
    }
}

function lineDistanceHTwoPointPlan(x1, y1, x2, y2, dOfLine, Pos, ScaleNormal, cm) {
        if (Pos == 1) /////1= L to R
            ////////2= R to L
            Text = "<text style=\"font-size:16px\" x=\"" + (x1 + 8) + "\" y=\"" + ((y1 - dOfLine) - 5) + "\" fill=\"blue\">" + Math.ceil(ScaleNormal) / cm + "</text>";
        else
            Text = "<text style=\"font-size:16px\" x=\"" + (x2 + 8) + "\" y=\"" + ((y2 - dOfLine) - 5) + "\" fill=\"blue\">" + Math.ceil(ScaleNormal) / cm + "</text>";

        Line = "<line x1=\"" + x1 + "\" x2=\"" + x2 + "\" y1=\"" + (y1 - dOfLine) + "\" y2=\"" + (y2 - dOfLine) + "\" style=\"stroke: rgb(38, 130, 161); stroke-width:0.5\" />";

        $('#AbnieShow').find('#svgLinesPlan').append(Line);
        $('#AbnieShow').find('#svgLinesPlan').append(Text);
        Line1 = "<line x1=\"" + (x1 - 2) + "\" x2=\"" + (x1 + 2) + "\" y1=\"" + ((y1 - dOfLine) + 2) + "\" y2=\"" + ((y1 - dOfLine) - 2) + "\" style=\"stroke: rgb(38, 130, 161); stroke-width:0.5\" />";
        $('#AbnieShow').find('#svgLinesPlan').append(Line1);
        Line1 = "<line x1=\"" + (x2 - 2) + "\" x2=\"" + (x2 + 2) + "\" y1=\"" + ((y2 - dOfLine) + 2) + "\" y2=\"" + ((y2 - dOfLine) - 2) + "\" style=\"stroke: rgb(38, 130, 161); stroke-width:0.5\" />";
        $('#AbnieShow').find('#svgLinesPlan').append(Line1);
        $('#AbnieShow').find('#svgContainerPlan').html($('#AbnieShow').find('#svgContainerPlan').html());
}


//////////////محاسبه فاصله بین دو نقطه
function calOfTwoPoint(x1,y1,x2,y2)
{
    fasele = Math.sqrt(Math.pow((x1 - x2),2) + Math.pow((y1 - y2),2));
    return fasele/100;
}



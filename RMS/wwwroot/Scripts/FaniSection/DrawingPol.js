function Pol(D, h, a1, a2, b1, b2, c1, c2, f, m, t, j, StartPointX, StartPointY, Scale, cm) {
    debugger;
    D *= Scale; h *= Scale; a1 *= Scale; a2 *= Scale; b1 *= Scale; b2 *= Scale; c1 *= Scale; c2 *= Scale;
    f *= Scale; m *= Scale; t *= Scale; j *= Scale;

    Fix = 30; Fix *= Scale;

    ScaleNormal=(1/Scale);
    lineDistanceH(StartPointX, StartPointY, StartPointX + a1, StartPointY, a1, 1, -10, ScaleNormal, cm);
    lineDistanceH(StartPointX + a1, StartPointY, StartPointX + a1 + b1, StartPointY, b1, 1, -10, ScaleNormal, cm);
    lineDistanceH(StartPointX + a1 + b1, StartPointY, StartPointX + a1 + b1 + a2, StartPointY, a2, 1, -10, ScaleNormal, cm);

    line(StartPointX, StartPointY, StartPointX + a1 + b1 + a2, StartPointY);////2
    line(StartPointX + a1 + b1 + a2, StartPointY, StartPointX + a1 + b1 + a2, StartPointY + m);////3
    line(StartPointX, StartPointY, StartPointX, StartPointY + m);//////1
    line(StartPointX, StartPointY + m, StartPointX + f, StartPointY + m);///4
    lineDistanceH(StartPointX, StartPointY + m, StartPointX + f, StartPointY + m, f, 1, 15, ScaleNormal, cm);
    hmin30 = h - Fix;
    line(StartPointX + a1 + b1, StartPointY, StartPointX + a1 + b1, StartPointY - hmin30);/////5
    line(StartPointX + a1 + b1, StartPointY - hmin30, StartPointX + a1 + b1 - b2, StartPointY - hmin30);///6
    lineDistanceH(StartPointX + a1 + b1, StartPointY - hmin30, StartPointX + a1 + b1 - b2, StartPointY - hmin30, b2, 2, 20, ScaleNormal, cm);

    line(StartPointX + a1 + b1, StartPointY - hmin30, StartPointX + a1 + b1, StartPointY - hmin30 - Fix);////////7
    line(StartPointX + a1 + b1, StartPointY - hmin30 - Fix, StartPointX + a1 + b1 - c1, StartPointY - hmin30 - Fix);////8
    lineDistanceH(StartPointX + a1 + b1, StartPointY - hmin30 - Fix - t, StartPointX + a1 + b1 - c1, StartPointY - hmin30 - t - Fix, c1, 2, -10, ScaleNormal, cm);

    line(StartPointX + a1 + b1 - c1, StartPointY - hmin30 - Fix, StartPointX + a1 + b1 - c1, StartPointY - hmin30 - Fix - t);////9
    ////خط مورب پایه کناری
    line(StartPointX + a1 + b1 - b2, StartPointY - hmin30, StartPointX + a1, StartPointY);////10
    x1Moarab = StartPointX + a1 + b1 - b2;
    y1Moarab = StartPointY - hmin30;
    x2Moarab = StartPointX + a1;
    y2Moarab = StartPointY;
    LPayeMoarab = Math.sqrt(Math.pow((x2Moarab - x1Moarab), 2) + Math.pow((y2Moarab - y1Moarab), 2));
    LPayeMoarab *= ScaleNormal;
    $('#HDFLPayeMoarab').val(LPayeMoarab.toFixed(0));
    ////////////

    tPlus30 = t + Fix;
    line(StartPointX + a1 + b1 - b2, StartPointY - hmin30, StartPointX + a1 + b1 - b2, StartPointY - hmin30 - tPlus30);////11
    line(StartPointX + a1 + b1 - b2, StartPointY - hmin30 - tPlus30, StartPointX + a1 + b1 - b2 + c2, StartPointY - hmin30 - tPlus30);/////12
    lineDistanceH(StartPointX + a1 + b1 - b2, StartPointY - hmin30 - tPlus30, StartPointX + a1 + b1 - b2 + c2, StartPointY - hmin30 - tPlus30, c2, 1, -10, ScaleNormal, cm);

    ////////////
    ColLeftX = StartPointX + a1 + b1 - b2 + c2 + j;
    ColLeftY = StartPointY - hmin30 - tPlus30;
    ToolDal = D + (2 * c1) - (2 * j);
    line(ColLeftX, ColLeftY, ColLeftX, ColLeftY + t);//////رسم دال13  
    line(ColLeftX, ColLeftY + t, ColLeftX + ToolDal, ColLeftY + t);////14
    line(ColLeftX, ColLeftY, ColLeftX + ToolDal, ColLeftY);////15
    lineDistanceH(ColLeftX + ToolDal, ColLeftY, ColLeftX + ToolDal + j, ColLeftY, j, 1, -10, ScaleNormal, cm);

    line(ColLeftX + ToolDal, ColLeftY, ColLeftX + ToolDal, ColLeftY + t);////16

    //////////////رسم ستون راست
    dOfTwoCol = D - (2 * a2);
    POfRightCol = a1 + b1 + a2 + dOfTwoCol;
    line(StartPointX + POfRightCol, StartPointY, StartPointX + POfRightCol + a2 + b1 + a1, StartPointY);////17
    line(StartPointX + POfRightCol, StartPointY, StartPointX + POfRightCol, StartPointY + m);////18
    line(StartPointX + POfRightCol, StartPointY + m, StartPointX + POfRightCol + a2 + b1 + a1, StartPointY + m);////19
    line(StartPointX + POfRightCol + a2 + b1 + a1, StartPointY + m, StartPointX + POfRightCol + a2 + b1 + a1, StartPointY);////20

    lineDistanceV(StartPointX + POfRightCol + a2 + b1 + a1 + 15, StartPointY + m, StartPointX + POfRightCol + a2 + b1 + a1 + 15, StartPointY, m, 1, ScaleNormal, cm);

    line(StartPointX + POfRightCol + a2, StartPointY, StartPointX + POfRightCol + a2, StartPointY - hmin30);////21

    lineDistanceH(StartPointX + a1 + b1, StartPointY, StartPointX + POfRightCol + a2, StartPointY, D, 1, -40, ScaleNormal, cm);

    line(StartPointX + POfRightCol + a2, StartPointY - hmin30, StartPointX + POfRightCol + a2 + b2, StartPointY - hmin30);///22
    line(StartPointX + POfRightCol + a2, StartPointY - hmin30, StartPointX + POfRightCol + a2, StartPointY - hmin30 - Fix);///23

    lineDistanceV(StartPointX + POfRightCol + a2 + b1 + a1 + 15, StartPointY, StartPointX + POfRightCol + a2 + b1 + a1 + 15, StartPointY - hmin30 - Fix, h, 1, ScaleNormal, cm);

    lineDistanceV(StartPointX + POfRightCol + a2 - 25, StartPointY - hmin30, StartPointX + POfRightCol + a2 - 25, StartPointY - hmin30 - Fix, 30, 1, 1, cm);

    line(StartPointX + POfRightCol + a2 + b2, StartPointY - hmin30, StartPointX + POfRightCol + a2 + b1, StartPointY);////24
    line(StartPointX + POfRightCol + a2 + b2, StartPointY - hmin30, StartPointX + POfRightCol + a2 + b2, StartPointY - hmin30 - tPlus30);////25

    line(StartPointX + POfRightCol + a2, StartPointY - hmin30 - Fix, StartPointX + POfRightCol + a2 + c1, StartPointY - hmin30 - Fix);////26
    line(StartPointX + POfRightCol + a2 + b2, StartPointY - hmin30 - tPlus30, StartPointX + POfRightCol + a2 - c2 + b2, StartPointY - hmin30 - tPlus30);////27
    line(StartPointX + POfRightCol + a2 - c2 + b2, StartPointY - hmin30 - tPlus30, StartPointX + POfRightCol + a2 - c2 + b2, StartPointY - hmin30 - Fix);///28
    lineDistanceV(StartPointX + POfRightCol + a1 + a2 - c2 + b2 - c1 + b1 + 15, StartPointY - hmin30 - tPlus30, StartPointX + POfRightCol + a1 + a2 - c2 + b2 - c1 + b1 + 15, StartPointY - hmin30 - Fix, t, 2, ScaleNormal, cm);
}

function Pol2Dahaneh(D, h, a1, a2, b1, b2, c1, c2, f, m, p1, p2, e, n, k, t, j, StartPointX, StartPointY, Scale, cm) {
    D *= Scale; h *= Scale; a1 *= Scale; a2 *= Scale; b1 *= Scale; b2 *= Scale; c1 *= Scale; c2 *= Scale;
    f *= Scale; m *= Scale; t *= Scale; j *= Scale; p1 *= Scale; p2 *= Scale; e *= Scale; n *= Scale; k *= Scale;

    Fix = 30; Fix *= Scale;

    ScaleNormal = (1 / Scale);
    lineDistanceH(StartPointX, StartPointY, StartPointX + a1, StartPointY, a1, 1, -10, ScaleNormal, cm);
    lineDistanceH(StartPointX + a1, StartPointY, StartPointX + a1 + b1, StartPointY, b1, 1, -10, ScaleNormal, cm);
    lineDistanceH(StartPointX + a1 + b1, StartPointY, StartPointX + a1 + b1 + a2, StartPointY, a2, 1, -10, ScaleNormal, cm);

    line(StartPointX, StartPointY, StartPointX + a1 + b1 + a2, StartPointY);////2
    line(StartPointX + a1 + b1 + a2, StartPointY, StartPointX + a1 + b1 + a2, StartPointY + m);////3
    line(StartPointX, StartPointY, StartPointX, StartPointY + m);//////1
    line(StartPointX, StartPointY + m, StartPointX + f, StartPointY + m);///4
    lineDistanceH(StartPointX, StartPointY + m, StartPointX + f, StartPointY + m, f, 1, 15, ScaleNormal, cm);
    hmin30 = h - Fix;
    line(StartPointX + a1 + b1, StartPointY, StartPointX + a1 + b1, StartPointY - hmin30);/////5
    line(StartPointX + a1 + b1, StartPointY - hmin30, StartPointX + a1 + b1 - b2, StartPointY - hmin30);///6
    lineDistanceH(StartPointX + a1 + b1, StartPointY - hmin30, StartPointX + a1 + b1 - b2, StartPointY - hmin30, b2, 2, -10, ScaleNormal, cm);

    line(StartPointX + a1 + b1, StartPointY - hmin30, StartPointX + a1 + b1, StartPointY - hmin30 - Fix);////////7
    line(StartPointX + a1 + b1, StartPointY - hmin30 - Fix, StartPointX + a1 + b1 - c1, StartPointY - hmin30 - Fix);////8
    lineDistanceH(StartPointX + a1 + b1, StartPointY - hmin30 - Fix - t, StartPointX + a1 + b1 - c1, StartPointY - hmin30 - t - Fix, c1, 2, -10, ScaleNormal, cm);

    line(StartPointX + a1 + b1 - c1, StartPointY - hmin30 - Fix, StartPointX + a1 + b1 - c1, StartPointY - hmin30 - Fix - t);////9
    //////////
    ////خط مورب پایه کناری
    line(StartPointX + a1 + b1 - b2, StartPointY - hmin30, StartPointX + a1, StartPointY);////10
    x1Moarab = StartPointX + a1 + b1 - b2;
    y1Moarab = StartPointY - hmin30;
    x2Moarab = StartPointX + a1;
    y2Moarab = StartPointY;
    LPayeMoarab = Math.sqrt(Math.pow((x2Moarab - x1Moarab), 2) + Math.pow((y2Moarab - y1Moarab), 2));
    LPayeMoarab *= ScaleNormal;
    $('#HDFLPayeMoarab').val(LPayeMoarab.toFixed(0));
    ///////////
    tPlus30 = t + Fix;
    line(StartPointX + a1 + b1 - b2, StartPointY - hmin30, StartPointX + a1 + b1 - b2, StartPointY - hmin30 - tPlus30);////11
    line(StartPointX + a1 + b1 - b2, StartPointY - hmin30 - tPlus30, StartPointX + a1 + b1 - b2 + c2, StartPointY - hmin30 - tPlus30);/////12
    lineDistanceH(StartPointX + a1 + b1 - b2, StartPointY - hmin30 - tPlus30, StartPointX + a1 + b1 - b2 + c2, StartPointY - hmin30 - tPlus30, c2, 1, -10, ScaleNormal, cm);

    ////////////رسم دال اول
    ColLeftX = StartPointX + a1 + b1 - b2 + c2 + j;
    ColLeftY = StartPointY - hmin30 - tPlus30;
    ToolDal = D + (2 * c1) - (2 * j);
    line(ColLeftX, ColLeftY, ColLeftX, ColLeftY + t);//////رسم دال13  
    line(ColLeftX, ColLeftY + t, ColLeftX + ToolDal, ColLeftY + t);////14
    line(ColLeftX, ColLeftY, ColLeftX + ToolDal, ColLeftY);////15
    lineDistanceH(ColLeftX + ToolDal, ColLeftY, ColLeftX + ToolDal + j, ColLeftY, j, 1, -10, ScaleNormal, cm);

    line(ColLeftX + ToolDal, ColLeftY, ColLeftX + ToolDal, ColLeftY + t);////16
    ////////////رسم دال دوم
    ColLeftX = (StartPointX + a1 + b1 - b2 + c2 + j) + (ToolDal)+(2*j);
    ColLeftY = StartPointY - hmin30 - tPlus30;
    //ToolDal = D + (2 * c1) - (2 * j);
    line(ColLeftX, ColLeftY, ColLeftX, ColLeftY + t);//////رسم دال13  
    line(ColLeftX, ColLeftY + t, ColLeftX + ToolDal, ColLeftY + t);////14
    line(ColLeftX, ColLeftY, ColLeftX + ToolDal, ColLeftY);////15
    lineDistanceH(ColLeftX + ToolDal, ColLeftY, ColLeftX + ToolDal + j, ColLeftY, j, 1, -10, ScaleNormal, cm);

    line(ColLeftX + ToolDal, ColLeftY, ColLeftX + ToolDal, ColLeftY + t);////16

    //////////////رسم ستون راست
    dOfTwoCol = D - (a2 + e);
    //POfRightCol = f + k + (2 * dOfTwoCol);
    POfRightCol = a1 + b1 + (2 * D) + p2 - a2;
    line(StartPointX + POfRightCol, StartPointY, StartPointX + POfRightCol + a2 + b1 + a1, StartPointY);////17
    line(StartPointX + POfRightCol, StartPointY, StartPointX + POfRightCol, StartPointY + m);////18
    line(StartPointX + POfRightCol, StartPointY + m, StartPointX + POfRightCol + a2 + b1 + a1, StartPointY + m);////19
    line(StartPointX + POfRightCol + a2 + b1 + a1, StartPointY + m, StartPointX + POfRightCol + a2 + b1 + a1, StartPointY);////20

    lineDistanceV(StartPointX + POfRightCol + a2 + b1 + a1 + 15, StartPointY + m, StartPointX + POfRightCol + a2 + b1 + a1 + 15, StartPointY, m, 1, ScaleNormal, cm);

    line(StartPointX + POfRightCol + a2, StartPointY, StartPointX + POfRightCol + a2, StartPointY - hmin30);////21

    lineDistanceH(StartPointX + a1 + b1, StartPointY, StartPointX + a1 + b1 + D, StartPointY, D, 1, -hmin30-10, ScaleNormal, cm);

    line(StartPointX + POfRightCol + a2, StartPointY - hmin30, StartPointX + POfRightCol + a2 + b2, StartPointY - hmin30);///22
    line(StartPointX + POfRightCol + a2, StartPointY - hmin30, StartPointX + POfRightCol + a2, StartPointY - hmin30 - Fix);///23

    lineDistanceV(StartPointX + POfRightCol + a2 + b1 + a1 + 15, StartPointY, StartPointX + POfRightCol + a2 + b1 + a1 + 15, StartPointY - hmin30 - Fix, h, 1, ScaleNormal, cm);

    lineDistanceV(StartPointX + POfRightCol + a2 + 15, StartPointY - hmin30, StartPointX + POfRightCol + a2 + 15, StartPointY - hmin30 - Fix, 30, 1, 1, cm);

    line(StartPointX + POfRightCol + a2 + b2, StartPointY - hmin30, StartPointX + POfRightCol + a2 + b1, StartPointY);////24
    line(StartPointX + POfRightCol + a2 + b2, StartPointY - hmin30, StartPointX + POfRightCol + a2 + b2, StartPointY - hmin30 - tPlus30);////25

    line(StartPointX + POfRightCol + a2, StartPointY - hmin30 - Fix, StartPointX + POfRightCol + a2 + c1, StartPointY - hmin30 - Fix);////26
    line(StartPointX + POfRightCol + a2 + b2, StartPointY - hmin30 - tPlus30, StartPointX + POfRightCol + a2 - c2 + b2, StartPointY - hmin30 - tPlus30);////27
    line(StartPointX + POfRightCol + a2 - c2 + b2, StartPointY - hmin30 - tPlus30, StartPointX + POfRightCol + a2 - c2 + b2, StartPointY - hmin30 - Fix);///28
    lineDistanceV(StartPointX + POfRightCol + a1 + a2 - c2 + b2 - c1 + b1 + 15, StartPointY - hmin30 - tPlus30, StartPointX + POfRightCol + a1 + a2 - c2 + b2 - c1 + b1 + 15, StartPointY - hmin30 - Fix, t, 2, ScaleNormal, cm);

    //////////////رسم ستون وسط
    dOfTwoCol = D - (a2 + e);
    //POfRightCol = f + dOfTwoCol;
    POfRightCol = a1 + b1 + D - e;
    line(StartPointX + POfRightCol, StartPointY, StartPointX + POfRightCol + k, StartPointY);////17
    line(StartPointX + POfRightCol, StartPointY, StartPointX + POfRightCol, StartPointY + n);////18
    line(StartPointX + POfRightCol, StartPointY + n, StartPointX + POfRightCol + k, StartPointY + n);////19
    line(StartPointX + POfRightCol + k, StartPointY + n, StartPointX + POfRightCol + k, StartPointY);////20

    lineDistanceV(StartPointX + POfRightCol + k + 15, StartPointY + n, StartPointX + POfRightCol + k + 15, StartPointY, n, 1, ScaleNormal, cm);

    line(StartPointX + POfRightCol + e, StartPointY, StartPointX + a1 + b1 + D, StartPointY - hmin30);////21

    lineDistanceH(StartPointX + POfRightCol, StartPointY, StartPointX + POfRightCol + e, StartPointY, e, 1, -10, ScaleNormal, cm);
    lineDistanceH(StartPointX + POfRightCol + e, StartPointY, StartPointX + POfRightCol + e + p1, StartPointY, p1, 1, -10, ScaleNormal, cm);
    lineDistanceH(StartPointX + POfRightCol + e+p1, StartPointY, StartPointX + POfRightCol + e + p1+e, StartPointY, e, 1, -10, ScaleNormal, cm);

    line(StartPointX + a1 + b1 + D, StartPointY - hmin30, StartPointX + a1 + b1 + D + p2, StartPointY - hmin30);///22
    line(StartPointX + POfRightCol + e, StartPointY - hmin30, StartPointX + POfRightCol + e, StartPointY - hmin30 - Fix);///23

    lineDistanceV(StartPointX + POfRightCol + (2 * e) + p1 + 15, StartPointY, StartPointX + POfRightCol + (2 * e) + p1 + 15, StartPointY - hmin30 - Fix, h, 1, ScaleNormal, cm);
    lineDistanceH(StartPointX + a1 + b1 + D, StartPointY - hmin30, StartPointX + a1 + b1 + D+p2, StartPointY - hmin30, p2, 1, -10, ScaleNormal, cm);

    /////////////خط مورب پایه میانی
    line(StartPointX + POfRightCol + e + p1, StartPointY - hmin30, StartPointX + a1 + b1 + D + p2, StartPointY);////24
    x1Moarab = StartPointX + POfRightCol + e + p1;
    y1Moarab = StartPointY - hmin30;
    x2Moarab = StartPointX + a1 + b1 + D + p2;
    y2Moarab = StartPointY;
    LKooleMoarab = Math.sqrt(Math.pow((x2Moarab - x1Moarab), 2) + Math.pow((y2Moarab - y1Moarab), 2));
    LKooleMoarab *= ScaleNormal;
    $('#HDFLKooleMoarab').val(LKooleMoarab.toFixed(0));
    ///////////

    line(StartPointX + a1 + b1 + D + p2, StartPointY - hmin30, StartPointX + a1 + b1 + D + p2, StartPointY - hmin30 - Fix);////25
    lineDistanceH(StartPointX + a1 + b1 + D, StartPointY - t - hmin30 - Fix, StartPointX + a1 + b1 + D + c1, StartPointY - t - hmin30 - Fix, c1, 1, -10, ScaleNormal, cm);
    lineDistanceH(StartPointX + POfRightCol, StartPointY + n, StartPointX + POfRightCol + k, StartPointY + n, k, 1, 15, ScaleNormal, cm);

    line(StartPointX + a1 + b1 + D, StartPointY - hmin30 - Fix, StartPointX + a1 + b1 + D + p2, StartPointY - hmin30 - Fix);////26
}

function Pol3Dahaneh(D, h, a1, a2, b1, b2, c1, c2, f, m, p1, p2, e, n, k, t, j, StartPointX, StartPointY, Scale, cm) {
    D *= Scale; h *= Scale; a1 *= Scale; a2 *= Scale; b1 *= Scale; b2 *= Scale; c1 *= Scale; c2 *= Scale;
    f *= Scale; m *= Scale; t *= Scale; j *= Scale; p1 *= Scale; p2 *= Scale; e *= Scale; n *= Scale; k *= Scale;

    Fix = 30; Fix *= Scale;
    
    ScaleNormal = (1 / Scale);
    lineDistanceH(StartPointX, StartPointY, StartPointX + a1, StartPointY, a1, 1, -10, ScaleNormal, cm);
    lineDistanceH(StartPointX + a1, StartPointY, StartPointX + a1 + b1, StartPointY, b1, 1, -10, ScaleNormal, cm);
    lineDistanceH(StartPointX + a1 + b1, StartPointY, StartPointX + a1 + b1 + a2, StartPointY, a2, 1, -10, ScaleNormal, cm);

    line(StartPointX, StartPointY, StartPointX + a1 + b1 + a2, StartPointY);////2
    line(StartPointX + a1 + b1 + a2, StartPointY, StartPointX + a1 + b1 + a2, StartPointY + m);////3
    line(StartPointX, StartPointY, StartPointX, StartPointY + m);//////1
    line(StartPointX, StartPointY + m, StartPointX + f, StartPointY + m);///4
    lineDistanceH(StartPointX, StartPointY + m, StartPointX + f, StartPointY + m, f, 1, 15, ScaleNormal, cm);
    hmin30 = h - Fix;
    line(StartPointX + a1 + b1, StartPointY, StartPointX + a1 + b1, StartPointY - hmin30);/////5
    line(StartPointX + a1 + b1, StartPointY - hmin30, StartPointX + a1 + b1 - b2, StartPointY - hmin30);///6
    lineDistanceH(StartPointX + a1 + b1, StartPointY - hmin30, StartPointX + a1 + b1 - b2, StartPointY - hmin30, b2, 2, +10, ScaleNormal, cm);

    line(StartPointX + a1 + b1, StartPointY - hmin30, StartPointX + a1 + b1, StartPointY - hmin30 - Fix);////////7
    line(StartPointX + a1 + b1, StartPointY - hmin30 - Fix, StartPointX + a1 + b1 - c1, StartPointY - hmin30 - Fix);////8
    lineDistanceH(StartPointX + a1 + b1, StartPointY - hmin30 - Fix - t, StartPointX + a1 + b1 - c1, StartPointY - hmin30 - t - Fix, c1, 2, -10, ScaleNormal, cm);

    line(StartPointX + a1 + b1 - c1, StartPointY - hmin30 - Fix, StartPointX + a1 + b1 - c1, StartPointY - hmin30 - Fix - t);////9
    ////خط مورب پایه کناری
    line(StartPointX + a1 + b1 - b2, StartPointY - hmin30, StartPointX + a1, StartPointY);////10
    x1Moarab = StartPointX + a1 + b1 - b2;
    y1Moarab = StartPointY - hmin30;
    x2Moarab = StartPointX + a1;
    y2Moarab = StartPointY;
    LPayeMoarab = Math.sqrt(Math.pow((x2Moarab - x1Moarab), 2) + Math.pow((y2Moarab - y1Moarab), 2));
    LPayeMoarab *= ScaleNormal;
    $('#HDFLPayeMoarab').val(LPayeMoarab.toFixed(0));
    ///////////
    tPlus30 = t + Fix;
    line(StartPointX + a1 + b1 - b2, StartPointY - hmin30, StartPointX + a1 + b1 - b2, StartPointY - hmin30 - tPlus30);////11
    line(StartPointX + a1 + b1 - b2, StartPointY - hmin30 - tPlus30, StartPointX + a1 + b1 - b2 + c2, StartPointY - hmin30 - tPlus30);/////12
    lineDistanceH(StartPointX + a1 + b1 - b2, StartPointY - hmin30 - tPlus30, StartPointX + a1 + b1 - b2 + c2, StartPointY - hmin30 - tPlus30, c2, 1, -10, ScaleNormal, cm);

    ////////////رسم دال اول
    ColLeftX = StartPointX + a1 + b1 - b2 + c2 + j;
    ColLeftY = StartPointY - hmin30 - tPlus30;
    ToolDal = D + (2 * c1) - (2 * j);
    line(ColLeftX, ColLeftY, ColLeftX, ColLeftY + t);//////رسم دال13  
    line(ColLeftX, ColLeftY + t, ColLeftX + ToolDal, ColLeftY + t);////14
    line(ColLeftX, ColLeftY, ColLeftX + ToolDal, ColLeftY);////15
    lineDistanceH(ColLeftX + ToolDal, ColLeftY, ColLeftX + ToolDal + j, ColLeftY, j, 1, -10, ScaleNormal, cm);

    line(ColLeftX + ToolDal, ColLeftY, ColLeftX + ToolDal, ColLeftY + t);////16
    ////////////رسم دال دوم
    ColLeftX = (StartPointX + a1 + b1 - b2 + c2 + j) + (ToolDal) + (2 * j);
    ColLeftY = StartPointY - hmin30 - tPlus30;
    line(ColLeftX, ColLeftY, ColLeftX, ColLeftY + t);//////رسم دال13  
    line(ColLeftX, ColLeftY + t, ColLeftX + ToolDal, ColLeftY + t);////14
    line(ColLeftX, ColLeftY, ColLeftX + ToolDal, ColLeftY);////15
    lineDistanceH(ColLeftX + ToolDal, ColLeftY, ColLeftX + ToolDal + j, ColLeftY, j, 1, -10, ScaleNormal, cm);

    line(ColLeftX + ToolDal, ColLeftY, ColLeftX + ToolDal, ColLeftY + t);////16
    ////////////رسم دال سوم
    ColLeftX = (StartPointX + a1 + b1 - b2 + c2 + j) + (2*ToolDal) + (4 * j);
    ColLeftY = StartPointY - hmin30 - tPlus30;
    line(ColLeftX, ColLeftY, ColLeftX, ColLeftY + t);//////رسم دال13  
    line(ColLeftX, ColLeftY + t, ColLeftX + ToolDal, ColLeftY + t);////14
    line(ColLeftX, ColLeftY, ColLeftX + ToolDal, ColLeftY);////15
    lineDistanceH(ColLeftX + ToolDal, ColLeftY, ColLeftX + ToolDal + j, ColLeftY, j, 1, -10, ScaleNormal, cm);

    line(ColLeftX + ToolDal, ColLeftY, ColLeftX + ToolDal, ColLeftY + t);////16

    //////////////رسم ستون راست
    dOfTwoCol = D - (a2 + e);
    dOfTwoCol2 = D - (2 * e);
    //POfRightCol = f + (2 * k) + (2 * dOfTwoCol) + dOfTwoCol2;
    POfRightCol = a1 + b1 + D + p2 + D +p2+D-a2;
    line(StartPointX + POfRightCol, StartPointY, StartPointX + POfRightCol + a2 + b1 + a1, StartPointY);////17
    line(StartPointX + POfRightCol, StartPointY, StartPointX + POfRightCol, StartPointY + m);////18
    line(StartPointX + POfRightCol, StartPointY + m, StartPointX + POfRightCol + a2 + b1 + a1, StartPointY + m);////19
    line(StartPointX + POfRightCol + a2 + b1 + a1, StartPointY + m, StartPointX + POfRightCol + a2 + b1 + a1, StartPointY);////20

    lineDistanceV(StartPointX + POfRightCol + a2 + b1 + a1 + 15, StartPointY + m, StartPointX + POfRightCol + a2 + b1 + a1 + 15, StartPointY, m, 1, ScaleNormal, cm);

    line(StartPointX + POfRightCol + a2, StartPointY, StartPointX + POfRightCol + a2, StartPointY - hmin30);////21

    lineDistanceH(StartPointX + a1 + b1, StartPointY, StartPointX + a1 + b1 + D, StartPointY, D, 1, -hmin30 + 10, ScaleNormal, cm);

    line(StartPointX + POfRightCol + a2, StartPointY - hmin30, StartPointX + POfRightCol + a2 + b2, StartPointY - hmin30);///22
    line(StartPointX + POfRightCol + a2, StartPointY - hmin30, StartPointX + POfRightCol + a2, StartPointY - hmin30 - Fix);///23

    lineDistanceV(StartPointX + POfRightCol + a2 + b1 + a1 + 15, StartPointY, StartPointX + POfRightCol + a2 + b1 + a1 + 15, StartPointY - hmin30 - Fix, h, 1, ScaleNormal, cm);

    lineDistanceV(StartPointX + POfRightCol + a2 + 15, StartPointY - hmin30, StartPointX + POfRightCol + a2 + 15, StartPointY - hmin30 - Fix, 30, 1, 1, cm);

    line(StartPointX + POfRightCol + a2 + b2, StartPointY - hmin30, StartPointX + POfRightCol + a2 + b1, StartPointY);////24
    line(StartPointX + POfRightCol + a2 + b2, StartPointY - hmin30, StartPointX + POfRightCol + a2 + b2, StartPointY - hmin30 - tPlus30);////25

    line(StartPointX + POfRightCol + a2, StartPointY - hmin30 - Fix, StartPointX + POfRightCol + a2 + c1, StartPointY - hmin30 - Fix);////26
    line(StartPointX + POfRightCol + a2 + b2, StartPointY - hmin30 - tPlus30, StartPointX + POfRightCol + a2 - c2 + b2, StartPointY - hmin30 - tPlus30);////27
    line(StartPointX + POfRightCol + a2 - c2 + b2, StartPointY - hmin30 - tPlus30, StartPointX + POfRightCol + a2 - c2 + b2, StartPointY - hmin30 - Fix);///28
    lineDistanceV(StartPointX + POfRightCol + a1 + a2 - c2 + b2 - c1 + b1 + 15, StartPointY - hmin30 - tPlus30, StartPointX + POfRightCol + a1 + a2 - c2 + b2 - c1 + b1 + 15, StartPointY - hmin30 - Fix, t, 2, ScaleNormal, cm);

    //////////////رسم ستون وسط اول 
    dOfTwoCol = D - (a2 + e);
    //POfRightCol = f + dOfTwoCol;
    POfRightCol = a1 + b1 + D - e;

    MoveToLeft = (p1 - p2) / 2;
    line(StartPointX + POfRightCol - MoveToLeft, StartPointY, StartPointX + POfRightCol - MoveToLeft + k, StartPointY);////17
    line(StartPointX + POfRightCol - MoveToLeft, StartPointY, StartPointX + POfRightCol - MoveToLeft, StartPointY + n);////18
    line(StartPointX + POfRightCol - MoveToLeft, StartPointY + n, StartPointX + POfRightCol - MoveToLeft + k, StartPointY + n);////19
    line(StartPointX + POfRightCol - MoveToLeft + k, StartPointY + n, StartPointX + POfRightCol - MoveToLeft + k, StartPointY);////20

    lineDistanceV(StartPointX + POfRightCol + k + 15, StartPointY + n, StartPointX + POfRightCol + k + 15, StartPointY, n, 1, ScaleNormal, cm);

    /////////////خط مورب پایه میانی
    line(StartPointX + POfRightCol - MoveToLeft + e, StartPointY, StartPointX + a1 + b1 + D, StartPointY - hmin30);////21
    x1Moarab = StartPointX + POfRightCol - MoveToLeft + e;
    y1Moarab = StartPointY;
    x2Moarab = StartPointX + a1 + b1 + D;
    y2Moarab = StartPointY - hmin30;
    LKooleMoarab = Math.sqrt(Math.pow((x2Moarab - x1Moarab), 2) + Math.pow((y2Moarab - y1Moarab), 2));
    LKooleMoarab *= ScaleNormal;
    $('#HDFLKooleMoarab').val(LKooleMoarab.toFixed(0));
    ///////////
    lineDistanceH(StartPointX + POfRightCol - MoveToLeft, StartPointY, StartPointX + POfRightCol - MoveToLeft + e, StartPointY, e, 1, -10, ScaleNormal, cm);
    lineDistanceH(StartPointX + POfRightCol - MoveToLeft + e, StartPointY, StartPointX + POfRightCol - MoveToLeft + e + p1, StartPointY, p1, 1, -10, ScaleNormal, cm);
    lineDistanceH(StartPointX + POfRightCol - MoveToLeft + e + p1, StartPointY, StartPointX + POfRightCol - MoveToLeft + e + p1 + e, StartPointY, e, 1, -10, ScaleNormal, cm);

    line(StartPointX + a1 + b1 + D, StartPointY - hmin30, StartPointX + a1 + b1 + D + p2, StartPointY - hmin30);///22
    line(StartPointX + POfRightCol + e, StartPointY - hmin30, StartPointX + POfRightCol + e, StartPointY - hmin30 - Fix);///23

    lineDistanceV(StartPointX + POfRightCol + (2 * e) + p1 + 15, StartPointY, StartPointX + POfRightCol + (2 * e) + p1 + 15, StartPointY - hmin30 - Fix, h, 1, ScaleNormal, cm);
    lineDistanceH(StartPointX + a1 + b1 + D, StartPointY - hmin30, StartPointX + a1 + b1 + D + p2, StartPointY - hmin30, p2, 1, 10, ScaleNormal, cm);

    line(StartPointX + POfRightCol - MoveToLeft + e + p1, StartPointY, StartPointX + a1 + b1 + D + p2, StartPointY - hmin30);////24
    line(StartPointX + a1 + b1 + D + p2, StartPointY - hmin30, StartPointX + a1 + b1 + D + p2, StartPointY - hmin30 - Fix);////25

    lineDistanceH(StartPointX + a1 + b1 + D, StartPointY - t - hmin30 - Fix, StartPointX + a1 + b1 + D + c1, StartPointY - t - hmin30 - Fix, c1, 1, -10, ScaleNormal, cm);
    lineDistanceH(StartPointX + POfRightCol - MoveToLeft, StartPointY + n, StartPointX + POfRightCol - MoveToLeft + k, StartPointY + n, k, 1, 15, ScaleNormal, cm);

    line(StartPointX + a1 + b1 + D, StartPointY - hmin30 - Fix, StartPointX + a1 + b1 + D + p2, StartPointY - hmin30 - Fix);////26

    ////////////// رسم ستون وسط دوم
    dOfTwoCol = D - (a2 + e);
    dOfTwoCol2 = D - (2 * e);
    //POfRightCol = f + k + dOfTwoCol + dOfTwoCol2;
    POfRightCol = a1 + b1 + D + p2+D-e;
    line(StartPointX + POfRightCol - MoveToLeft, StartPointY, StartPointX + POfRightCol - MoveToLeft + k, StartPointY);////17
    line(StartPointX + POfRightCol - MoveToLeft, StartPointY, StartPointX + POfRightCol - MoveToLeft, StartPointY + n);////18
    line(StartPointX + POfRightCol - MoveToLeft, StartPointY + n, StartPointX + POfRightCol - MoveToLeft + k, StartPointY + n);////19
    line(StartPointX + POfRightCol - MoveToLeft + k, StartPointY + n, StartPointX + POfRightCol - MoveToLeft + k, StartPointY);////20

    //lineDistanceV(StartPointX + POfRightCol + k + 15, StartPointY + n, StartPointX + POfRightCol + k + 15, StartPointY, n, 1, ScaleNormal, cm);
    line(StartPointX + POfRightCol - MoveToLeft + e, StartPointY, StartPointX + a1 + b1 + (2 * D) + p2, StartPointY - hmin30);////21

    //lineDistanceH(StartPointX + POfRightCol, StartPointY, StartPointX + POfRightCol + e, StartPointY, e, 1, -10, ScaleNormal, cm);
    //lineDistanceH(StartPointX + POfRightCol + e, StartPointY, StartPointX + POfRightCol + e + p1, StartPointY, p1, 1, -10, ScaleNormal, cm);
    //lineDistanceH(StartPointX + POfRightCol + e + p1, StartPointY, StartPointX + POfRightCol + e + p1 + e, StartPointY, e, 1, -10, ScaleNormal, cm);

    line(StartPointX + a1 + b1 + (2*D) + p2, StartPointY - hmin30, StartPointX + a1 + b1 + (2*D) + (2*p2), StartPointY - hmin30);///22
    line(StartPointX + POfRightCol + e, StartPointY - hmin30, StartPointX + POfRightCol + e, StartPointY - hmin30 - Fix);///23

    //lineDistanceV(StartPointX + POfRightCol + (2 * e) + p1 + 15, StartPointY, StartPointX + POfRightCol + (2 * e) + p1 + 15, StartPointY - hmin30 - 30, h, 1, ScaleNormal, cm);
    //lineDistanceH(StartPointX + a1 + b1 + D, StartPointY - hmin30, StartPointX + a1 + b1 + D + p2, StartPointY - hmin30, p2, 1, -10, ScaleNormal, cm);

    line(StartPointX + POfRightCol - MoveToLeft + e + p1, StartPointY, StartPointX + a1 + b1 + (2 * D) + (2 * p2), StartPointY - hmin30);////24
    line(StartPointX + a1 + b1 + (2 * D) + (2 * p2), StartPointY - hmin30, StartPointX + a1 + b1 + (2 * D) + (2 * p2), StartPointY - hmin30 - Fix);////25

    //lineDistanceH(StartPointX + a1 + b1 + D, StartPointY - t - hmin30 - 30, StartPointX + a1 + b1 + D + c1, StartPointY - t - hmin30 - 30, c1, 1, -10, ScaleNormal, cm);
    //lineDistanceH(StartPointX + POfRightCol, StartPointY + n, StartPointX + POfRightCol + k, StartPointY + n, k, 1, 15, ScaleNormal, cm);

    line(StartPointX + a1 + b1 + p2 + (2 * D), StartPointY - hmin30 - Fix, StartPointX + a1 + b1 + (2 * D) + (2 * p2), StartPointY - hmin30 - Fix);////26
}

function DivarBali(h, x, b, f, m, StartPointX, StartPointY, Scale, cm) {
    h *= Scale; x *= Scale; b *= Scale; f *= Scale; m *= Scale;
    Fix1 = 35; Fix1 *= Scale;
    Fix2 = 20; Fix2 *= Scale;
    Fix3 = 5; Fix3 *= Scale;
    fu1 = (h / 3) + x + Fix1;
    fu2 = (x / 2);

    ScaleNormal = (1 / Scale);

    line(StartPointX, StartPointY, StartPointX + f, StartPointY);////1
    line(StartPointX, StartPointY, StartPointX, StartPointY + m);////2
    line(StartPointX, StartPointY + m, StartPointX + f, StartPointY + m);////3
    line(StartPointX + f, StartPointY, StartPointX + f, StartPointY + m);////4

    lineDistanceH(StartPointX, StartPointY + m, StartPointX + f, StartPointY + m, f, 1, 20, ScaleNormal, cm);
    lineDistanceV(StartPointX + f + 15, StartPointY, StartPointX + f + 15, StartPointY + m, m, 2, ScaleNormal, cm);

    lineDistanceH(StartPointX, StartPointY, StartPointX + fu1, StartPointY, fu1, 1, -20, ScaleNormal, cm);
    lineDistanceH(StartPointX + fu1, StartPointY, StartPointX + fu1 + b, StartPointY, b, 1, -20, ScaleNormal, cm);



    line(StartPointX + fu1, StartPointY, StartPointX + fu1, StartPointY - h);////5
    lineDistanceV(StartPointX + fu1 + 15, StartPointY, StartPointX + fu1 + 15, StartPointY - h, h, 1, ScaleNormal, cm);

    line(StartPointX + fu1, StartPointY - h, StartPointX + fu1, StartPointY - h - Fix2);////6
    lineDistanceV(StartPointX + fu1 + 15, StartPointY - h, StartPointX + fu1 + 15, StartPointY - h - Fix2, Fix2, 1, ScaleNormal, cm);

    line(StartPointX + fu1, StartPointY - h - Fix2, StartPointX + fu1 - Fix1, StartPointY - h - Fix2);////7
    lineDistanceH(StartPointX + fu1, StartPointY - h - Fix2, StartPointX + fu1 - Fix1, StartPointY - h - Fix2, Fix1, 2, -10, ScaleNormal, cm);

    line(StartPointX + fu1, StartPointY - h, StartPointX + fu1 - Fix1, StartPointY - h);////8
    line(StartPointX + fu1 - Fix1, StartPointY - h, StartPointX + fu1 - Fix1, StartPointY - h - Fix2);////9

    line(StartPointX + fu1 - Fix1, StartPointY - h, StartPointX + fu1 - Fix1 - x, StartPointY - h + fu2);////10
    lineDistanceH(StartPointX + fu1 - Fix1, StartPointY - h, StartPointX + fu1 - Fix1 - x, StartPointY - h, x, 2, -10 - Fix2, ScaleNormal, cm);
    lineDistanceV(StartPointX + fu1 - Fix1 - x - 25, StartPointY - h, StartPointX + fu1 - Fix1 - x - 25, StartPointY - h + fu2, (x / 2), 2, ScaleNormal, cm);

    line(StartPointX, StartPointY, StartPointX + fu1 - Fix1 - x, StartPointY - h + fu2);////11
}

function DrawPos1(D, t1, t2, t3, c1, j, StartPointX, StartPointY, Scale, Meyar) {
    fix1=0.08*100;fix1*=Scale;
    D *= Scale; t1 *= Scale; t2 *= Scale; t3 *= Scale; c1 *= Scale; j *= Scale;
    Tool = D + ((2 * c1) - (2 * j) - fix1);
    ToolEnteha = Tool - (t2 + t3);

    line(StartPointX, StartPointY, StartPointX, StartPointY - t1);////1
    line(StartPointX, StartPointY - t1, StartPointX + t2, StartPointY - t1);////2
    line(StartPointX + t2, StartPointY - t1, StartPointX + t2 + t3, StartPointY - t1 + t3);////3
    line(StartPointX + t2 + t3, StartPointY - t1 + t3, StartPointX + t2 + t3 + ToolEnteha, StartPointY - t1 + t3);////4
    line(StartPointX + t2 + t3 + ToolEnteha, StartPointY - t1 + t3, StartPointX + t2 + t3 + ToolEnteha, StartPointY - t1 + t3 - t1);////5
}
function DrawPos2(D, t1, t2, t3, c1, j, StartPointX, StartPointY, Scale, Meyar) {
    fix1 = 0.08 * 100; fix1 *= Scale;
    D *= Scale; t1 *= Scale; t2 *= Scale; t3 *= Scale; c1 *= Scale; j *= Scale;
    Tool = D + ((2 * c1) - (2 * j) - fix1);
    ToolEnteha = Tool - (t2 + t3);
    line(StartPointX, StartPointY, StartPointX, StartPointY + t1);////1
    line(StartPointX, StartPointY + t1, StartPointX + ToolEnteha, StartPointY + t1);////2
    line(StartPointX + ToolEnteha, StartPointY + t1 , StartPointX + ToolEnteha + t3, StartPointY + t1 - t3);////3
    line(StartPointX + ToolEnteha + t3, StartPointY + t1 - t3, StartPointX + ToolEnteha + t3+t2, StartPointY + t1 - t3);////4
    line(StartPointX + ToolEnteha + t3 + t2, StartPointY + t1 - t3 + t2, StartPointX  + t3+ t2 + ToolEnteha, StartPointY + t1 - t3 + t2-t1);////5
}

function DrawPos4(D, t1, c1, j, StartPointX, StartPointY, Scale, Meyar) {
    fix1 = 0.08 * 100; fix1 *= Scale;
    D *= Scale; t1 *= Scale; c1 *= Scale; j *= Scale;
    Tool = D + ((2 * c1) - (2 * j) - fix1);
    line(StartPointX, StartPointY, StartPointX, StartPointY - t1);////1
    line(StartPointX, StartPointY - t1, StartPointX + Tool, StartPointY - t1);////2
    line(StartPointX + Tool, StartPointY - t1, StartPointX + Tool, StartPointY);////3
}

function DrawPos7Left(b2, c1, c2, t, StartPointX, StartPointY, Scale, Meyar) {
    fix1 = 0.3 * 100; fix1 *= Scale;
    fix2 = 0.05 * 100; fix2 *= Scale;
    fix3 = 0.04 * 100; fix3 *= Scale;

    b2 *= Scale; c1 *= Scale; c2 *= Scale; t *= Scale;
    Ertefa = t + fix1;
    line(StartPointX, StartPointY, StartPointX, StartPointY + Ertefa);////1
    line(StartPointX, StartPointY, StartPointX + c2, StartPointY);////2
    line(StartPointX, StartPointY + Ertefa, StartPointX + b2, StartPointY + Ertefa);////3
    line(StartPointX + b2, StartPointY + Ertefa, StartPointX + b2, StartPointY + Ertefa - fix1);////4
    line(StartPointX + b2, StartPointY + Ertefa - fix1, StartPointX + b2 - c1, StartPointY + Ertefa - fix1);////5
    line(StartPointX + c2, StartPointY, StartPointX + c2, StartPointY + t);////6
    //////////////////////////////
    line(StartPointX + fix3, StartPointY + fix2, StartPointX + c2 - fix3, StartPointY + fix2);////1
    line(StartPointX + fix3, StartPointY + fix2, StartPointX + fix3, StartPointY + (t + fix1-fix2));////2
    line(StartPointX + c2 - fix3, StartPointY + fix2, StartPointX + c2 - fix3, StartPointY + (t + fix1 - fix2));////3
    line(StartPointX + fix3, StartPointY + (t + fix1 - fix2), StartPointX + b2-fix3, StartPointY + (t + fix1 - fix2));////4
    line(StartPointX + (b2 - fix3), StartPointY + t + fix2, StartPointX + (b2 - fix3), StartPointY + t + fix1 - fix2);////5
    line(StartPointX + fix3, StartPointY + t + fix2, StartPointX + (b2 - fix3), StartPointY + t + fix2);////6
}

function DrawPos7Right(D, j, b2, c1, c2, t, StartPointX, StartPointY, Scale, Meyar) {
    fix1 = 0.3 * 100; fix1 *= Scale;
    fix2 = 0.05 * 100; fix2 *= Scale;
    fix3 = 0.04 * 100; fix3 *= Scale;
    fix4 = 0.08 * 100; fix4 *= Scale;
    D *= Scale; b2 *= Scale; c1 *= Scale; c2 *= Scale; t *= Scale;
    Tool = D + c2 + (2 * c1);
    Ertefa = t + fix1;
    line(StartPointX + Tool, StartPointY, StartPointX + Tool, StartPointY + Ertefa - fix1);////1
    line(StartPointX + Tool, StartPointY, StartPointX + Tool + c2, StartPointY);////2
    line(StartPointX + Tool - c1, StartPointY + Ertefa, StartPointX + Tool + b2 - c1, StartPointY + Ertefa);////3
    line(StartPointX + Tool - c1, StartPointY + Ertefa, StartPointX + Tool - c1, StartPointY + Ertefa - fix1);////4
    line(StartPointX + Tool - c1, StartPointY + Ertefa - fix1, StartPointX + Tool , StartPointY + Ertefa - fix1);////5
    line(StartPointX + Tool + c2, StartPointY, StartPointX + Tool + c2, StartPointY + t+fix1);////6
    //////////////////////////////
    line(StartPointX + Tool + fix3, StartPointY + fix2, StartPointX + Tool + c2 - fix3, StartPointY + fix2);////1
    line(StartPointX + Tool + fix3, StartPointY + fix2, StartPointX + Tool + fix3, StartPointY + (t + fix1 - fix2));////2
    line(StartPointX + Tool + c2 - fix3, StartPointY + fix2, StartPointX + Tool + c2 - fix3, StartPointY + (t + fix1-fix2));////3
    line(StartPointX + Tool - c1 + fix3, StartPointY + (t + fix1 - fix2), StartPointX + Tool - c1 - fix3 + b2, StartPointY + (t + fix1 - fix2));////4
    line(StartPointX + Tool - c1 + fix3, StartPointY + t + fix2, StartPointX + Tool - c1 + fix3, StartPointY + t + fix1 - fix2);////5
    line(StartPointX + Tool - c1 + fix3, StartPointY + fix2 + t, StartPointX + Tool - c1 - fix3 + b2, StartPointY + fix2 + t);////
}

function DrawDal(D, c1, j,t, StartPointX, StartPointY, Scale, Meyar) {
    D *= Scale; t1 *= Scale; c1 *= Scale; j *= Scale; t *= Scale;
    Tool = D + ((2 * c1) - (2 * j));
    line(StartPointX, StartPointY, StartPointX, StartPointY - t);////1
    line(StartPointX, StartPointY - t, StartPointX + Tool, StartPointY - t);////2
    line(StartPointX + Tool, StartPointY - t, StartPointX + Tool, StartPointY);////3
    line(StartPointX, StartPointY, StartPointX + Tool, StartPointY);////4
}

function line(x1, y1, x2, y2) {
    Line = "<line x1=\"" + x1 + "\" x2=\"" + x2 + "\" y1=\"" + y1 + "\" y2=\"" + y2 + "\" style=\"stroke: rgb(0,0,0); stroke-width:1\" />";
    $('#svgLines').append(Line);
    $('#svgContainerPol').html($('#svgContainerPol').html());
}
function lineDistanceH(x1, y1, x2, y2, distance, Pos, dOfLine, ScaleNormal, cm) {
    if (distance != 0) {
        if (Pos == 1) /////1= L to R
            ////////2= R to L
            Text = "<text style=\"font-size:20px\" x=\"" + (x1 + ((distance / 2) + 8)) + "\" y=\"" + ((y1 + dOfLine) - 5) + "\" fill=\"blue\">" + parseInt(distance * ScaleNormal) / cm + "</text>";
        else
            Text = "<text style=\"font-size:20px\" x=\"" + (x2 + ((distance / 2) + 8)) + "\" y=\"" + ((y2 + dOfLine) - 5) + "\" fill=\"blue\">" + parseInt(distance * ScaleNormal) / cm + "</text>";
        Line = "<line x1=\"" + x1 + "\" x2=\"" + x2 + "\" y1=\"" + (y1 + dOfLine) + "\" y2=\"" + (y2 + dOfLine) + "\" style=\"stroke: rgb(0,0,0); stroke-width:0.5\" />";
        $('#svgLines').append(Line);
        $('#svgLines').append(Text);
        Line1 = "<line x1=\"" + (x1 - 2) + "\" x2=\"" + (x1 + 2) + "\" y1=\"" + ((y1 + dOfLine) + 2) + "\" y2=\"" + ((y1 + dOfLine) - 2) + "\" style=\"stroke: rgb(0,0,0); stroke-width:0.5\" />";
        $('#svgLines').append(Line1);
        Line1 = "<line x1=\"" + (x2 - 2) + "\" x2=\"" + (x2 + 2) + "\" y1=\"" + ((y2 + dOfLine) + 2) + "\" y2=\"" + ((y2 + dOfLine) - 2) + "\" style=\"stroke: rgb(0,0,0); stroke-width:0.5\" />";
        $('#svgLines').append(Line1);
        $('#svgContainerPol').html($('#svgContainerPol').html());
    }
}
function lineDistanceV(x1, y1, x2, y2, distance, Pos, ScaleNormal, cm) {
    if (distance != 0) {
        if (Pos == 1) /////1= L to R
            ////////2= R to L
            Text = "<text style=\"font-size:20px\" x=\"" + (x1 + 17) + "\" y=\"" + (y1 - ((distance / 2))) + "\" fill=\"blue\">" + (distance * ScaleNormal) / cm + "</text>";
        else
            Text = "<text style=\"font-size:20px\" x=\"" + (x2 + 17) + "\" y=\"" + (y2 - ((distance / 2))) + "\" fill=\"blue\">" + (distance * ScaleNormal) / cm + "</text>";
        Line = "<line x1=\"" + x1 + "\" x2=\"" + x2 + "\" y1=\"" + y1 + "\" y2=\"" + y2 + "\" style=\"stroke: rgb(0,0,0); stroke-width:0.5\" />";
        $('#svgLines').append(Line);
        $('#svgLines').append(Text);
        Line1 = "<line x1=\"" + (x1 - 2) + "\" x2=\"" + (x1 + 2) + "\" y1=\"" + (y1 + 2) + "\" y2=\"" + (y1 - 2) + "\" style=\"stroke: rgb(0,0,0); stroke-width:0.5\" />";
        $('#svgLines').append(Line1);
        Line1 = "<line x1=\"" + (x2 - 2) + "\" x2=\"" + (x2 + 2) + "\" y1=\"" + (y2 + 2) + "\" y2=\"" + (y2 - 2) + "\" style=\"stroke: rgb(0,0,0); stroke-width:0.5\" />";
        $('#svgLines').append(Line1);
        $('#svgContainerPol').html($('#svgContainerPol').html());
    }
}

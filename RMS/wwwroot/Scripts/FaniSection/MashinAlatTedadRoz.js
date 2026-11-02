var Operation = null;

$(document).ready(function () {
    GetViewBag();
});

function GetViewBag() {
    $.ajax({
        type: "Get",
        url: "/BaseInfo/GetViewBag",
        data: JSON.stringify(''),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {

            viewBag = response.viewBag;
            lstNoeFBs = response.lstNoeFBs;

            $('#HDFBarAvordUserID').val(viewBag.baId);
            $('#HDFYear').val(viewBag.year);

            let str = "";
            let selectedId = null;

            $.each(lstNoeFBs, function (index, item) {

                // اولین رادیو انتخاب شود
                let checked = index === 0 ? "checked" : "";

                // اگر اولین آیتم است مقدارش را در متغیر ذخیره کن
                if (index === 0) {
                    selectedId = item.id;
                }

                str += `
                                <label style="margin-right:10px;">
                                <input type="radio" name="radType" onchange="FBChange(${item.id})" value="${item.id}" ${checked}/>
                                ${item.name}
                                </label>
                                `;
            });

            $('#HDFNoeFB').val(selectedId);

            GetMashinAlatRoz(0);
            GetGeneralProjectTiming();


        },
        error: function (response) {
            toastr.error('خطا', 'خطا');
        }
    });
}


function GetMashinAlatRoz(TedadAmelMoaser) {
    BarAvordUserId = $('#HDFBarAvordUserID').val();
    Year = $('#HDFYear').val();
    NoeFB = parseInt($('#HDFNoeFB').val());

    var vardata = new Object();
    vardata.BarAvordId = BarAvordUserId;
    vardata.NoeFB = NoeFB;
    vardata.Year = Year;

    $.ajax({
        type: "POST",
        url: '/MashinAlat/GetInfoFBOfOperationAndAnalizBaha',
        dataType: "json",
        data: JSON.stringify(vardata),
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            //var xmlDoc = $.parseXML(data);
            //var xml = $(xmlDoc);
            var OperationName = data.getOperationNameHasValue; //xml.find("tblOperationName");
            Operation = data.lstOperation; //xml.find("tblOperation");
            var NiroZaribKarKard = data.operationNameAnalizBahaWithOperation; //xml.find("tblNiroZaribKarKard");
            str = '';
            NiroType = 'نیروی انسانی_ماشین آلات_مصالح_حمل'.split('_');

            NoeMashinAlats = [];
            $.each(NiroZaribKarKard, function () {
                NoeMashinAlat = this.noeMashinAlatName;
                if ($.inArray(NoeMashinAlat, NoeMashinAlats) == -1) {
                    NoeMashinAlats.push(NoeMashinAlat);
                }
            });

            str =
                `<div class='row col-6'>
                    <div class='row col-12'>
                        <div class='row col-12' style='border: 1px solid #a0b2cd;'>
                        <div class='col-2'>کد عامل</div>
                        <div class='col-6'>عنوان ماشین</div>
                        <div class='col-1'>تعداد مورد نیاز</div>
                        <div class='col-2'>تعداد روز کاری</div>
                        <div class='col-1'>عامل موثر</div>
                    </div>
               </div>`;
            CountOperationName = 0;
            arrMaxTedadSaat = [];
            $.each(OperationName, function () {

                MaxTedadSaat = 0;
                OperationLatinName = this.operationLatinName;
                OperationNodeIdCurrent = this.operationId;
                OperationNodeParentIdCurrent = this.parentId;

                str += `<div class='row'>
                            <div style='border: 1px solid #a0b2cd;' class='col-md-12'>
                                ${OperationLatinName}
                            </div>
                        </div>`;

                MaxAmelMoaser = this.maxMeghdarMeghias;
                if (TedadAmelMoaser != 0) {
                    MaxAmelMoaser = this.maxMeghdarMeghias / TedadAmelMoaser;
                }
                CodeAmelMoaser = this.maxCodeAmelMoaser;

                dMaxAmelMoaser = parseFloat(MaxAmelMoaser);

                AmelMeghdar = [];
                for (var k = 0; k < NoeMashinAlats.length; k++) {

                    ExtraMeghdar = 0;
                    count = 0;
                    NoeMashinAlatCurrent = [];

                    $.each(NiroZaribKarKard, function () {
                        NoeMashinAlatNameCurrent = this.noeMashinAlatName;
                        CodeAmel = this.codeAmel;
                        if (NoeMashinAlats[k] == NoeMashinAlatNameCurrent) {
                            NoeMashinAlatCurrent.push(CodeAmel);
                        }
                    });

                    DailyTime = parseFloat($('#txtDailyTime').val().trim());


                    $.each(NiroZaribKarKard, function () {

                        OperationNameCurrent = this.operationExceptOpNodeLatinName;
                        if (OperationLatinName == OperationNameCurrent) {
                            NoeMashinAlatName = this.noeMashinAlatName;
                            CodeAmel = this.codeAmel;
                            count++;
                            if (NoeMashinAlatName == NoeMashinAlats[k]) {
                                ZaribKarKard = this.zaribKarKard;
                                Title = this.title;
                                if (CodeAmel.trim() != CodeAmelMoaser.trim()) {
                                    finalExtra = 0;
                                    if (ExtraMeghdar != 0) {
                                        if (ZaribKarKard != 0) {
                                            finalExtra = ExtraMeghdar / ZaribKarKard;
                                        }
                                    }


                                    var intSection = 0;
                                    var floatSection = 0;
                                    if (dMaxAmelMoaser != 0) {
                                        intSection = Math.floor((parseFloat(this.meghdarMeghias)) / dMaxAmelMoaser);
                                        floatSection = (parseFloat(this.meghdarMeghias)) / dMaxAmelMoaser - intSection;
                                    }


                                    if (intSection >= 1) {
                                        str += `<div class='row col-12'>
                                                    <div class='row col-12' style='border: 1px solid #a0b2cd;'>
                                                       <div class='col-2'>${CodeAmel}</div>
                                                       <div class='col-6'>${Title}</div>
                                                       <div class='col-1'>${intSection}</div>
                                                       <div class='col-2'>${Math.ceil(dMaxAmelMoaser / DailyTime)}</div>
                                                       <div class='col-1'>خیر</div>
                                                    </div>
                                                 </div>`;
                                        //str += '<div><input onclick=\"changeAmelMoaser(' + CodeAmel + ',' + OperationNodeIdCurrent + ')\" type=\"radio\" name=\"G' + OperationNodeIdCurrent + '\"/></div>';

                                        MaxTedadSaat = dMaxAmelMoaser > MaxTedadSaat ? dMaxAmelMoaser : MaxTedadSaat;
                                        ExtraMeghdar = floatSection * ZaribKarKard * dMaxAmelMoaser;
                                    }
                                    else {
                                        str += `<div class='row col-12'>
                                                    <div class='row col-12' style='border: 1px solid #a0b2cd;'>
                                                        <div class='col-2'>${CodeAmel}</div>
                                                        <div class='col-6'>${Title}</div>
                                                        <div class='col-1'>1</div>
                                                        <div class='col-2'>${Math.ceil(this.meghdarMeghias / DailyTime)}</div>
                                                        <div class='col-1'>خیر</div>
                                                     </div>
                                                  </div>`;
                                        //str += '<div>' + CodeAmel + '_' + 1 + '_' + Title +'_'+$(this).find("MeghdarMeghias").text()+ '_' + Math.ceil($(this).find("MeghdarMeghias").text() / 8) + '</div>';
                                        //str += '<div><input onclick=\"changeAmelMoaser(' + CodeAmel + ',' + OperationNodeIdCurrent + ')\" type=\"radio\" name=\"G' + OperationNodeIdCurrent + '\"/></div>';
                                    }

                                    if (ExtraMeghdar != 0) {
                                        finalExtra = ExtraMeghdar / ZaribKarKard;
                                        str += `<div class='row col-12'>
                                                    <div class='row col-12' style='border: 1px solid #a0b2cd;'>
                                                        <div class='col-2'>${CodeAmel}</div>
                                                        <div class='col-6'>${Title}</div>
                                                        <div class='col-1'>1</div>
                                                        <div class='col-2'>${Math.ceil(finalExtra / DailyTime)}</div>
                                                        <div class='col-1'>خیر</div>
                                                     </div>
                                                  </div>`;
                                        //str += '<div>' + CodeAmel + '_' + 1 + '_' + Title + '_' + finalExtra+'_' + Math.ceil(finalExtra / 8) + '</div>';
                                        //str += '<div><input onclick=\"changeAmelMoaser(' + CodeAmel + ',' + OperationNodeIdCurrent + ')\" type=\"radio\" name=\"G' + OperationNodeIdCurrent + '\"/></div>';
                                    }
                                }
                                else {
                                    intSection = Math.floor((parseFloat(this.meghdarMeghias)) / dMaxAmelMoaser);
                                    str += `<div class='row col-12'>
                                                <div class='row col-12' style='border: 1px solid #a0b2cd;'>
                                                    <div class='col-2'>${CodeAmel}</div>
                                                    <div class='col-6'>${Title}</div>
                                                    <div class='col-1'>${intSection}</div>
                                                    <div class='col-2'>${Math.ceil(dMaxAmelMoaser / DailyTime)}</div>
                                                    <div class='col-1'>بله</div>
                                                </div>
                                            </div>`;
                                    //str += '<div>' + CodeAmel + '_' + intSection + '_' + Title + '_' + $(this).find("MeghdarMeghias").text() + '_' + Math.ceil(MaxAmelMoaser / 8) + '</div>';
                                    //str += '<div><input type=\"text\" onchange=\"GetMashinAlatRoz($(this).val())\"/></div>';
                                    //str += '<div><input onclick=\"changeAmelMoaser(' + CodeAmel + ',' + OperationNodeIdCurrent + ')\" type=\"radio\" name=\"G' + OperationNodeIdCurrent + '\"/></div>';
                                }
                            }
                        }
                    });
                }
                arrMaxTedadSaat[CountOperationName++] = OperationNodeIdCurrent + "_" +
                    OperationNodeParentIdCurrent + "_" + OperationLatinName + "_" + MaxTedadSaat;
                str += '</div>';
                str += `<div class='col-6'>
                            <div class='row col-12'>
                                <div class='col-12' style='border: 1px solid #a0b2cd;'>موجودی کارگاه</div>
                             </div>
                          <div class='row col-12'>
                            <div class='row col-12' style='border: 1px solid #a0b2cd;'>
                                <div class='col-1'>نوع ماشین</div>
                                <div class='col-8'>
                                <div class='row col-12'>
                                <div class='col-8'>انتخاب ماشین</div>
                                <div class='col-2'>تعداد</div>
                                <div class='col-2'>ساعت</div>
                                </div>
                            </div>
                            <div class='col-1'>تعداد روز</div>
                            <div class='col-2'>ظرفیت</div>
                          </div>
                        </div>`;

                $.each(OperationName, function () {
                    OperationLatinName = this.operationLatinName;
                    OperationNodeIdCurrent = this.operationId;
                    OperationNodeParentIdCurrent = this.parentId;
                    str += `<div class='row'>
                                <div style='border: 1px solid #a0b2cd;' class='col-8'>${OperationLatinName}</div>
                                <div class='col-4'>
                                    <input style='border:none;display:none' onclick='CalculateOfTime()' type='button' value='محاسبه'/>
                                </div>
                            </div>`;

                    NoeMashinAlatSelected = [];

                    $.each(NiroZaribKarKard, function () {
                        OperationNameCurrent = this.operationExceptOpNodeLatinName;
                        if (OperationLatinName == OperationNameCurrent) {
                            NoeMashinAlatName = this.noeMashinAlatName;
                            MashinTypeName = this.mashinTypeName;
                            if (jQuery.inArray(NoeMashinAlatName, NoeMashinAlatSelected) < 0) {
                                NoeMashinAlatSelected.push(NoeMashinAlatName);
                                str += `<div class='row' style='border: 1px solid #a0b2cd;padding:2px 0px'>
                                            <div class='col-1' style='padding:2px 0px'>${MashinTypeName}</div>
                                            <div id='div_${OperationNameCurrent}_${NoeMashinAlatName}' class='col-md-8'>`;

                                $.each(NiroZaribKarKard, function () {
                                    CodeAmel = this.codeAmel;
                                    NoeMashinAlatNameCurrent = this.noeMashinAlatName;
                                    OperationNameCurrent1 = this.operationExceptOpNodeLatinName;
                                    Title = this.title;
                                    if (NoeMashinAlatNameCurrent == NoeMashinAlatName && OperationNameCurrent1 == OperationNameCurrent) {
                                        str += `<div class='row col-12' style='padding:2px 0px'>
                                                <div class='col-8'><input id='ck_${OperationNameCurrent1}_${CodeAmel}' type='checkbox' onchange='SelectMashinClick($(this))'/>${Title}<br/></div>
                                                <div class='col-2' style='padding: 0px 2px;'>
                                                    <input id='txt_${OperationNameCurrent1}_${CodeAmel}' onchange='CalculateOfTime();' style='text-align:center;display:none;padding: 0px;' type='text' value='1' class='form-control input-sm'/>
                                                </div>
                                                <div class='col-2' style='padding: 0px 2px;'>
                                                    <input id='txtSaat_${OperationNameCurrent1}_${CodeAmel}' onchange='CalculateOfTime();' style='text-align:center;display:none;padding: 0px;' type='text' value='8' class='form-control input-sm'/>
                                                </div>
                                                </div>`;
                                    }
                                });
                                str += `</div>
                                        <div class='col-1' style='padding: 2px 2px;'>
                                            <input id='txtDay_${OperationNameCurrent}_${NoeMashinAlatName}' style='text-align:center;padding: 0px;' type='text' value='' class='form-control input-sm'/>
                                        </div>
                                        <div class='col-2' style='padding: 2px 2px;'>
                                            <input id='txtCapasity_${OperationNameCurrent}_${NoeMashinAlatName}' style='text-align:center;padding: 0px;' type='text' value='' class='form-control input-sm'/>
                                        </div>
                                        </div>`;
                            }
                        }
                    });
                });
                str += '</div>';
            });

            $('#divTedadDastgah').html(str);
            DrawChart(Operation, arrMaxTedadSaat);
        },
        error: function (msg) {
            toastr.info('بروز مشکل در بارگذاری اطلاعات، مجددا امتحان نمایید', 'خطا');
        }
    });
}

var arr = [];

function DrawChart(Operation, arrMaxTedadSaat) {

    debugger;

    var promise1 = new Promise(function (resolve, reject) {

        getElements(Operation);

        resolve('');
    });

    promise1.then(function (result) {

        debugger;


        getNodes(arrMaxTedadSaat);

        return new Promise(function (resolve, reject) {
            resolve(result);
        });
    })
        .then(function (result) {
            generateParents();

        });
}

function getElements(Operation) {
    var elements = '';
    $.each(Operation, function () {
        var Id = this.id; //$(this).find("Id").text();
        var OperationName = this.operationName;// $(this).find("OperationName").text();
        var LatinName = this.latinName; //$(this).find("LatinName").text();
        var ParentId = this.parentId;//  $(this).find("ParentId").text();

        elements += `<tr>
            <td>
                <label>${OperationName}</label>
            </td>
            <td>
                <div id='div${Id}' parentid='${ParentId}' title='0' class='bar context-menu-one' width='0' style='width:0px;'>
                </div>
             </td>
            </tr>`;
    });

    debugger;


    $("#chart-table").append(elements);

}

function CalculateOfTime() {
    BarAvordUserId = $('#HDFBarAvordUserID').val();
    Year = $('#HDFYear').val();
    NoeFB = parseInt($('#HDFNoeFB').val());

    var vardata = new Object();
    vardata.BarAvordId = BarAvordUserId;
    vardata.NoeFB = NoeFB;
    vardata.Year = Year;

    $.ajax({
        type: "POST",
        url: '/MashinAlat/GetInfoFBOfOperationAndAnalizBaha',
        dataType: "json",
        data: JSON.stringify(vardata),
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            //var xmlDoc = $.parseXML(data.d);
            //var xml = $(xmlDoc);
            //var OperationName = xml.find("tblOperationName");
            //var Operation = xml.find("tblOperation");
            //var NiroZaribKarKard = xml.find("tblNiroZaribKarKard");

            var OperationName = data.getOperationNameHasValue; //xml.find("tblOperationName");
            Operation = data.lstOperation; //xml.find("tblOperation");
            var NiroZaribKarKard = data.operationNameAnalizBahaWithOperation; //xml.find("tblNiroZaribKarKard");
            $.each(OperationName, function () {
                OperationLatinName = this.operationLatinName; //$(this).find("OperationLatinName").text();
                OperationNodeIdCurrent = this.operationId;// $(this).find("OperationId").text();
                OperationNodeParentIdCurrent = this.parentId;// $(this).find("ParentId").text();

                NoeMashinAlatSelected = [];

                NoeMashinAlatName = '';
                MaxDay = 0;
                $.each(NiroZaribKarKard, function () {
                    OperationNameCurrent = this.operationExceptOpNodeLatinName; //$(this).find("OperationExceptOpNodeLatinName").text();
                    if (OperationLatinName == OperationNameCurrent) {
                        NoeMashinAlatName = this.noeMashinAlatName;// $(this).find("NoeMashinAlatName").text();
                        MashinTypeName = this.mashinTypeName;// $(this).find("MashinTypeName").text();
                        sumZaribKarkard = 0;
                        if (jQuery.inArray(NoeMashinAlatName, NoeMashinAlatSelected) < 0) {
                            NoeMashinAlatSelected.push(NoeMashinAlatName);
                            $.each(NiroZaribKarKard, function () {
                                MeghdarMeghias = this.meghdarMeghias;// $(this).find("MeghdarMeghias").text();
                                ZaribKarKard = this.zaribKarKard;// $(this).find("ZaribKarKard").text();
                                NoeMashinAlatNameCurrent = this.noeMashinAlatName;// $(this).find("NoeMashinAlatName").text();
                                OperationNameCurrent1 = this.operationExceptOpNodeLatinName;// $(this).find("OperationExceptOpNodeLatinName").text();
                                Title = this.title;// $(this).find("Title").text();
                                if (NoeMashinAlatNameCurrent == NoeMashinAlatName && OperationNameCurrent1 == OperationNameCurrent) {
                                    sumZaribKarkard += ZaribKarKard * MeghdarMeghias
                                }
                            });
                            SumKarKard = 0;
                            $('#div_' + OperationNameCurrent + '_' + NoeMashinAlatName + ' input').each(function () {
                                CurrentId = $(this).attr('id').split('_');
                                if (CurrentId[0] == 'ck') {
                                    if ($(this).is(':checked')) {
                                        CodeAmelSelected = CurrentId[2];
                                        CurrentVal = parseFloat($('#txt_' + OperationNameCurrent + '_' + CurrentId[2]).val());
                                        CurrentSaat = parseFloat($('#txtSaat_' + OperationNameCurrent + '_' + CurrentId[2]).val());
                                        ZaribKarKard = 0;
                                        $.each(NiroZaribKarKard, function () {
                                            CodeAmel = this.codeAmel;// $(this).find("CodeAmel").text().trim();
                                            if (CodeAmelSelected == CodeAmel) {
                                                ZaribKarKard = this.zaribKarKard;// $(this).find("ZaribKarKard").text();
                                            }
                                        });
                                        SumKarKard += ZaribKarKard * CurrentVal * CurrentSaat;
                                    }
                                }
                            });
                            CodeAmelCurrent = this.codeAmel; //$(this).find("CodeAmel").text();
                            if (SumKarKard != 0) {
                                Day = Math.round(sumZaribKarkard / SumKarKard);
                                MaxDay = Day > MaxDay ? Day : MaxDay;
                                $('#txtDay_' + OperationLatinName + '_' + NoeMashinAlatName).val(Day);
                            }
                            else
                                $('#txtDay_' + OperationLatinName + '_' + NoeMashinAlatName).val(0);
                        }
                    }
                });

                $.each(NiroZaribKarKard, function () {
                    OperationNameCurrent = this.operationExceptOpNodeLatinName;// $(this).find("OperationExceptOpNodeLatinName").text();
                    if (OperationLatinName == OperationNameCurrent) {
                        NoeMashinAlatName = this.noeMashinAlatName;// $(this).find("NoeMashinAlatName").text();
                        Day = $('#txtDay_' + OperationLatinName + '_' + NoeMashinAlatName).val();
                        if (MaxDay != 0)
                            $('#txtCapasity_' + OperationLatinName + '_' + NoeMashinAlatName).val(((Day / MaxDay) * 100).toFixed(2));
                        else
                            $('#txtCapasity_' + OperationLatinName + '_' + NoeMashinAlatName).val(0);
                    }
                });
            });
        },
        error: function () {
        }
    });
}

function SelectMashinClick(object) {
    object.parent().parent().find('input[type="text"]').each(function () {
        if (object.is(':checked')) {
            {
                $(this).css('display', 'block');
                DailyTime = parseFloat($('#txtDailyTime').val().trim());
                Id = $(this).attr('id').split('_');
                $('#txtSaat_' + Id[1] + '_' + Id[2]).val(DailyTime);
            }
        }
        else {
            $(this).css('display', 'none');
        }
    });
    CalculateOfTime();
}

function changeAmelMoaser(CodeAmel, OperationNodeIdCurrent) {
    //projectId = $('#hdnProjectId').val();
    BarAvordUserId = $('#HDFBarAvordUserID').val();

    var vardata = new Object();
    vardata.BarAvordUserId = BarAvordUserId;
    vardata.CodeAmel = CodeAmel;
    vardata.OperationId = OperationNodeIdCurrent;

    $.ajax({
        type: "POST",
        url: "/MashinAlat/changeAmelMoaser",
        data: JSON.stringify(vardata),
        //data: '{ProjectId: ' + projectId
        //    + ',CodeAmel:' + "'" + CodeAmel + "'" + ',OperationId:' + OperationNodeIdCurrent + '}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            info = response.d;
            if (info) {
                GetMashinAlatRoz(0);
            }
        },
        error: function (response) {
        }
    });
}

function getNodes(arrMaxTedadSaat) {
    debugger;
    for (var i = 0; i < arrMaxTedadSaat.length; i++) {
        var arr1 = arrMaxTedadSaat[i].split('_');
        Id = arr1[0];
        ParentId = arr1[1];
        NoeOp = arr1[2];
        Saat = arr1[3];

        DailyTime = parseFloat($('#txtDailyTime').val());
        day = Math.ceil(Saat / DailyTime);

        $('#chart-table #div' + Id).css('width', day * Saat + 'px');
        $('#chart-table #div' + Id).attr('width', day * Saat);
        $('#chart-table #div' + Id).attr('title', day);

        generateElementSizes(Id, true);
    }
}

function generateElementSizes(id, isFirstId) {
    var chartId = $('#chart-table');

    var element = chartId.find('#div' + id);

    var parentId = element.attr('parentid');

    if (!isFirstId)
        arr.push(id);

    if (parentId == '0') {
        return;
    }
    return generateElementSizes(parentId, false);
}

function generateParents() {

    var chartId = $('#chart-table');

    var uniqueArr = arr.filter(onlyUnique);

    $.each(uniqueArr, function (i, val) {

        var width = 0;
        chartId.find('div[parentid="' + val + '"]').each(function (index, item) {

            width += parseInt($(item).attr('width'));

        });
        var element = chartId.find('#div' + val);
        element.attr('width', width);
        element.css('width', width + 'px');
        element.attr('title', width);

    });
}

function onlyUnique(value, index, self) {
    return self.indexOf(value) === index;
}

function generateSplit(object) {

    $('#divSplit').remove();

    var divSplit =
        `<div id="divSplit">
            <input type="text" id="txtNumber" placeholder="تعداد" class="txtNumber"/>
            <input type="hidden" id="hdnElementId" value="${$(object).attr("id")}"/>
            <label>مانده:</label><label id="lblRemained">${$(object).attr("width")}</label>
        </div>`;

    $(divSplit).insertAfter($(object));

    $('#txtNumber').focus();

    $('#txtNumber').bind('keyup paste', function () {
        this.value = this.value.replace(/[^0-9]/g, '');

        var elementId = $('#divSplit').find('#hdnElementId').val();

        var width = parseInt($('#' + elementId).attr("width"));

        var number = 0;
        if (this.value != '')
            number = parseInt(this.value);

        var r = width - number;
        if (r <= 0) {
            this.value = '';

            $('#lblRemained').text(width);
        }
        else
            $('#lblRemained').text(r);
    });

    $('#txtNumber').keypress(function (e) {
        var elementId = $('#divSplit').find('#hdnElementId').val();

        var width = parseInt($('#' + elementId).attr("width"));

        var number = parseInt($(this).val());

        if (e.which === 13) {
            e.preventDefault();

            $('#' + elementId).attr("width", number);
            $('#' + elementId).css("width", number + 'px');
            $('#' + elementId).attr("title", number);

            var newWidth = (width - number);
            var newElement = '<div id="new' + $('#' + elementId).parent().find('div:first-child').attr("id") + Math.random().toString().substr(2) + '" parentid="' + $('#' + elementId).attr("parentid") + '" title="' + newWidth + '" class="bar context-menu-one" width="' + newWidth + '" style="width:' + newWidth + 'px;">';
            $(newElement).insertAfter($('#divSplit').parent().find('#' + elementId));

            $('#divSplit').remove();
        }
        else {
        }
    });
}


function GetGeneralProjectTiming() {
    //var projectId = $('#<%=hdnProjectId.ClientID%>').val();
    BarAvordUserId = $('#HDFBarAvordUserID').val();
    var vardata = new Object();
    vardata.BarAvordUserId = BarAvordUserId;

    //var jsonParameters = JSON.parse('{"ProjectId":"' + projectId + '"}');

    $.ajax({
        type: "POST",
        url: '/MashinAlat/GetGeneralProjectTiming',
        dataType: "json",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var xmlDoc = $.parseXML(data.d);
            var xml = $(xmlDoc);
            var tblGeneralProjectTiming = xml.find("tblGeneralProjectTiming");

            //var projectId = tblGeneralProjectTiming.find("ProjectId").text();
            var startDate = tblGeneralProjectTiming.find("StartDate").text();
            var countDaies = tblGeneralProjectTiming.find("CountDaies").text();
            var courseCountDaies = tblGeneralProjectTiming.find("CourseCountDaies").text();
            var holyCourseCountDaies = tblGeneralProjectTiming.find("HolyCourseCountDaies").text();

            $('#txtStartDate').val(startDate);
            $('#txtCountDaies').val(countDaies);
            $('#txtCourseCountDaies').val(courseCountDaies);
            $('#txtHolyCourseCountDaies').val(holyCourseCountDaies);

        },
        error: function (msg) {
            toastr.error('بروز مشکل در بارگذاری اطلاعات، مجددا امتحان نمایید', 'خطا');
        }
    });
}

function CheckForInsertOrUpdateGeneralProjectTiming() {

    if (!checkfields()) {
        toastr.warning('لطفا فیلدهای ورودی را کنترل نمایید', 'هشدار');
        return;
    }

    debugger;

    //var projectId = $('#hdnProjectId').val();
    BarAvordUserId = $('#HDFBarAvordUserID').val();

    //var jsonParameters = JSON.parse('{"ProjectId":"' + projectId + '"}');

    var vardata = new Object();
    vardata.BarAvordUserId = BarAvordUserId;

    $.ajax({
        type: "POST",
        url: '/MashinAlat/GetGeneralProjectTiming',
        dataType: "json",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            //var xmlDoc = $.parseXML(data.d);
            //var xml = $(xmlDoc);

            debugger;
            var tblGeneralProjectTiming = data; //xml.find("tblGeneralProjectTiming");

            if (tblGeneralProjectTiming.length == 0) {
                SaveGeneralProjectTiming('insert', '');
            }
            else {
                var id = tblGeneralProjectTiming.id;
                SaveGeneralProjectTiming('update', id);
            }

            //var projectId = tblGeneralProjectTiming.find("ProjectId").text();

            //if (id == '')
            //else
        },
        error: function (msg) {
            toastr.error('بروز مشکل در بارگذاری اطلاعات، مجددا امتحان نمایید', 'خطا');
        }
    });
}

function SaveGeneralProjectTiming(action, id) {

    BarAvordUserId = $('#HDFBarAvordUserID').val();

    //var projectId = $('#<%=hdnProjectId.ClientID%>').val();
    var startDate = $('#txtStartDate').val();
    var countDaies = $('#txtCountDaies').val();
    var courseCountDaies = $('#txtCourseCountDaies').val();
    var holyCourseCountDaies = $('#txtHolyCourseCountDaies').val();

    var vardata = new Object();
    vardata.StartDate = startDate;
    vardata.CountDaies = countDaies;
    vardata.CourseCountDaies = courseCountDaies;
    vardata.HolyCourseCountDaies = holyCourseCountDaies;

    debugger;
    var url = '';
    //var parameters = '{';

    if (action == 'insert') {

        vardata.BarAvordId = BarAvordUserId;

        //parameters += '"ProjectId":"' + projectId + '"';
        url = '/MashinAlat/SaveGeneralProjectTiming';
    }
    else if (action == 'update') {

        vardata.Id = id;

        //parameters += '"Id":"' + id + '"';
        url = '/MashinAlat/UpdateGeneralProjectTiming';
    }

    //parameters += ',"StartDate":"' + startDate + '","CountDaies":"' + countDaies + '","CourseCountDaies":"' + courseCountDaies + '","HolyCourseCountDaies":"' + holyCourseCountDaies + '"}';

    //var jsonParameters = JSON.parse(parameters);

    $.ajax({
        type: "POST",
        url: url,
        dataType: "json",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data == "OK")
                toastr.success('اطلاعات موردنظر با موفقیت ذخیره گردید', 'موفقیت');
            else
                toastr.error('بروز مشکل در ذخیره اطلاعات، مجددا امتحان نمایید', 'خطا');

        },
        error: function (msg) {
            toastr.error('بروز مشکل در ذخیره اطلاعات، مجددا امتحان نمایید', 'خطا');
        }
    });
}
function checkfields() {
    debugger;
    BarAvordUserId = $('#HDFBarAvordUserID').val();
    //var projectId = $('#<%=hdnProjectId.ClientID%>').val();
    var startDate = $('#txtStartDate').val();
    var countDaies = $('#txtCountDaies').val();
    var courseCountDaies = $('#txtCourseCountDaies').val();
    var holyCourseCountDaies = $('#txtHolyCourseCountDaies').val();

    if (BarAvordUserId == '' || startDate == '' || countDaies == '' || courseCountDaies == '' || holyCourseCountDaies == '')
        return false;

    return true;
}

function GetStatusOfDateInPeriod() {
    BarAvordUserId = $('#HDFBarAvordUserID').val();

    //var projectId = $('#hdnProjectId').val();
    var startDate = $('#txtStartDate').val();
    var countDaies = $('#txtCountDaies').val();
    var courseCountDaies = $('#txtCourseCountDaies').val();
    var holyCourseCountDaies = $('#txtHolyCourseCountDaies').val();

    debugger;
    var vardata = new Object();
    vardata.BarAvordUserId = BarAvordUserId;
    vardata.StartDate = startDate;
    vardata.ProjectLen = countDaies;
    vardata.CourseCountDaies = courseCountDaies;
    vardata.HolyCourseCountDaies = holyCourseCountDaies;

    //var parameters = '{"ProjectId":"' + projectId + '","StartDate":"' + startDate + '","ProjectLen":"' + countDaies + '","CourseCountDaies":"' + courseCountDaies + '","HolyCourseCountDaies":"' + holyCourseCountDaies + '"}';

    //var jsonParameters = JSON.parse(parameters);

    $.ajax({
        type: "POST",
        url: '/MashinAlat/GetStatusOfDateInPeriod',
        dataType: "json",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            debugger;
            //var xmlDoc = $.parseXML(data.d);
            //var xml = $(xmlDoc);
            var tblZamanbandiProjeTafsiliTemp = data; //xml.find("tblZamanbandiProjeTafsiliTemp");

            var elements = '';

            elements +=
                `<tr>
                    <th>تاریخ</th>
                    <th>عنوان روز</th>
                    <th>نوع تعطیلی</th>
                    <th>وضعیت</th>
                </tr>`;
            $.each(tblZamanbandiProjeTafsiliTemp, function () {
                //var id = $(this).find("Id").text();
                var date = this.date;
                var dayName = this.dayName;
                var noeTatili = this.noeTatili;
                var active = this.active;
                debugger;
                isChecked = active == 'True' ? 'checked' : '';
                elements +=
                    `<tr>
                        <td>
                            <span class="info-date">${date}</span>
                        </td>
                        <td>
                            <span class="info-dayName">${dayName}</span>
                        </td>
                        <td>
                            <span class="info-noeTatili">${GetNoeTatiliTitle(noeTatili)}</span>
                            <input type="hidden" id="hdnNoeTatiliNumber" value="${noeTatili}"/>
                        </td>
                        <td>
                            <input type="checkbox" ${isChecked} onchange="SaveDayStateAndUpdateData(${this});">
                        </td>
                    </tr>`;
            });

            $("#Info-table").empty().append(elements);

        },
        error: function (msg) {
            toastr.error('بروز مشکل در بارگذاری اطلاعات، مجددا امتحان نمایید', 'خطا');
        }
    });
}

function GetNoeTatiliTitle(noeTatili) {
    var title = '';
    switch (noeTatili) {
        case '0':
            title = 'روز عادی';
            break;
        case '1':
            title = 'تعطیل کارگاهی';
            break;
        case '2':
            title = 'تعطیل رسمی';
            break;
    }
    return title;
}

function SaveDayStateAndUpdateData(chk) {

    BarAvordUserId = $('#HDFBarAvordUserID').val();

    //var projectId = $('#hdnProjectId').val();
    var tr = $(chk).parent().parent();
    var date = tr.find('.info-date').text();
    var hdnNoeTatiliNumber = tr.find('#hdnNoeTatiliNumber').val();
    var activeState = $(chk).is(':checked').toString();

    var vardata = new Object();
    vardata.BarAvordUserId = BarAvordUserId;
    vardata.Date = date;
    vardata.State = hdnNoeTatiliNumber;
    vardata.ActiveDeActive = activeState;

    //var parameters = '{"ProjectId":"' + projectId + '","Date":"' + date + '","State":"' + hdnNoeTatiliNumber + '","ActiveDeActive":"' + activeState + '"}';

    //var jsonParameters = JSON.parse(parameters);

    $.ajax({
        type: "POST",
        url: '/MashinAlat/SaveDayState',
        dataType: "json",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.d == "OK") {
                toastr.success('اطلاعات موردنظر با موفقیت ذخیره گردید', 'موفقیت');
                GetStatusOfDateInPeriod();
            }
            else {
                toastr.error('بروز مشکل در ذخیره اطلاعات، مجددا امتحان نمایید', 'خطا');
            }
        },
        error: function (msg) {
            toastr.error('بروز مشکل در بارگذاری اطلاعات، مجددا امتحان نمایید', 'خطا');
        }
    });
}
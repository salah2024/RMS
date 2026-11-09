function GetSumOfItems() {
    BarAvordUserID = $('#HDFBarAvordUserID').val();
    Year = $('#HDFYear').val();
    NoeFB = parseInt($('#HDFNoeFB').val());
    var vardata = new Object();
    vardata.Year = Year;
    vardata.NoeFB = NoeFB;
    vardata.BarAvordUserId = BarAvordUserID;
    $.ajax({
        type: "POST",
        url: '/RizMetreUser/GetSumOfItems',
        dataType: "json",
        //data: '{Year:' + "'1397'" + ',NoeFB:' + "'eec10c4b-5452-4677-a22b-ab5ea9b4e3f0'"
        //    + ',BarAvordUserId:' + BarAvordUserID + '}',
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var xmlDoc = $.parseXML(data);
            var xml = $(xmlDoc);
            var tblOperationSumMeghdarAndRial = xml.find("tblOperationSumMeghdarAndRial");
            var tree = $('#jstree');
            var id;
            var meghdar;
            var meghdarRiali;
            $.each(tblOperationSumMeghdarAndRial, function () {

                id = $(this).find("Id").text();

                meghdar = ($(this).find("Meghdar").text().trim() != "" ? $(this).find("Meghdar").text().trim() : "0");
                meghdarRiali = ($(this).find("MeghdarRiali").text().trim() != "" ? Math.round($(this).find("MeghdarRiali").text().trim()) : "0");

                $('#jstree').find('#span' + id).text(" - " + "مقدار: " + meghdar + " - " + "مقدار ریالی: " + meghdarRiali);

                if (meghdarRiali > 0)
                    $('#jstree').find('#span' + id).parent().css("font-weight", "bold");
            });
        },
        error: function (msg) {
            toastr.error('مشکل در حذف', 'خطا');
        }
    });
}

function OpenConditionDetails(object, ConditionGroupId, ItemsFBShomareh) {

    newConditionGroupId = ConditionGroupId + ItemsFBShomareh;
    BarAvordUserId = $('#HDFBarAvordUserID').val();
    var vardata = new Object();
    vardata.BarAvordUserId = BarAvordUserId;
    vardata.strFBShomareh = ItemsFBShomareh;
    vardata.ConditionGroupId = ConditionGroupId;
    if (object.is(':checked')) {
        $('#divConditionGroup' + newConditionGroupId).slideDown(1000);
        object.attr('checked', false);
    }
    else {
        $('#divConditionGroup' + newConditionGroupId + ' input[type="radio"]:checked').each(function () {
            fullId = this.id;
            var ThisId = fullId.substring(2);
            deleteItemsCondition(ThisId, ConditionGroupId, ItemsFBShomareh, 1,false,null);
            //objectid = $(this).attr('id');
            //id = objectid.substring(2, objectid.length);
            //$.ajax({
            //    type: "POST",
            //    url: "/ItemsHasConditionAddedToFB/DeleteItemsHasConditionAddedToFBAndRizMetre",
            //    data: JSON.stringify(vardata),
            //    contentType: "application/json; charset=utf-8",
            //    dataType: "json",
            //    success: function (response) {
            //        info = response.split('_');
            //        if (info[0] == "OK") {
            //            OperationId = $('#HDFOperationId').val();
            //            GetRizMetreUsers();
            //            toastr.success('اضافه بهای آیتم انتخابی حذف گردید', 'موفقیت');
            //        }
            //        else
            //            toastr.error('مشکل در حذف اضافه بها', 'خطا');
            //    },
            //    error: function (response) {
            //        toastr.error('مشکل در حذف اضافه بها', 'خطا');
            //    }
            //});
        });
    }
}

function deleteItemsCondition(Id, ConditionGroupId, ItemsFBShomareh, LevelNumber, hasRelated, ObjRelated) {


    debugger;
    BarAvordUserId = $('#HDFBarAvordUserID').val();
    OperationId = $('#HDFOperationId').val();
    Year = $('#HDFYear').val();
    NoeFB = parseInt($('#HDFNoeFB').val());

    var vardata = new Object();
    vardata.BarAvordId = BarAvordUserId;
    vardata.strFBShomareh = ItemsFBShomareh;
    vardata.ConditionGroupId = ConditionGroupId;
    vardata.LevelNumber = LevelNumber;
    vardata.Year = Year;
    vardata.NoeFB = NoeFB;

    $.ajax({
        type: "POST",
        url: "/ItemsHasConditionAddedToFB/DeleteItemsHasConditionAddedToFBAndRizMetre",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            info = response.split('_');

            if (info[0] == "OK") {

                debugger;
                if (hasRelated) {

                    ConditionGroupId = ObjRelated.attr('data-conditiongroup');

                    var vardata1 = new Object();
                    vardata1.BarAvordId = BarAvordUserId;
                    vardata1.strFBShomareh = ItemsFBShomareh;
                    vardata1.ConditionGroupId = ConditionGroupId;
                    vardata1.LevelNumber = LevelNumber;
                    vardata1.Year = Year;
                    vardata1.NoeFB = NoeFB;


                    $.ajax({
                        type: "POST",
                        url: "/ItemsHasConditionAddedToFB/DeleteItemsHasConditionAddedToFBAndRizMetre",
                        data: JSON.stringify(vardata1),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response1) {
                            debugger;

                            info = response1.split('_');
                            if (info[0] == "OK") {

                                ObjRelated.attr('checked', false);

                                ObjRelated.removeAttr('disabled');

                                var id = $(ObjRelated).attr('id');
                                $('#loading_' + id).remove();
                                CheckCC(Id, ItemsFBShomareh, LevelNumber);
                            }
                            else
                                toastr.error('مشکل در حذف اضافه بها', 'خطا');
                        },
                        error: function (response) {
                            toastr.error('مشکل در حذف اضافه بها', 'خطا');
                        }
                    });
                }
                else {
                    CheckCC(Id, ItemsFBShomareh, LevelNumber);

                }



            }
            else
                toastr.error('مشکل در حذف اضافه بها', 'خطا');
        },
        error: function (response) {
            toastr.error('مشکل در حذف اضافه بها', 'خطا');
        }
    });
}

function PreChechedOnChange(object, Id, ConditionGroupId, ItemsFBShomareh, LevelNumber) {


    debugger;

    var $obj = $(object);

    // غیرفعال کردن
    $obj.prop('disabled', true);

    // اضافه کردن waiting
    var $loading = $('<span>', {
        id: 'loading_' + $obj.attr('id'),
        class: 'my-loading'
    });

    $obj.after($loading);


    BarAvordUserId = $('#HDFBarAvordUserID').val();
    OperationId = $('#HDFOperationId').val();
    Year = $('#HDFYear').val();
    NoeFB = parseInt($('#HDFNoeFB').val());

    var vardata = new Object();
    vardata.Id = Id;
    vardata.NoeFB = NoeFB;

    $.ajax({
        type: "POST",
        url: "/ConditionContext/CheckItemHasConditionContext",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            debugger;
            if (response != null) {

                IHC_CC_Related = response.ihC_CC_Related;
                ItemRelated = response.itemRelated;

                objChecked = $obj.prop('checked');
                if (!objChecked) {
                    if (IHC_CC_Related != '') {
                        ObjRelated = $('#CK' + IHC_CC_Related);
                        ///////
                        ///////
                        CheckedOnChange(
                            object,
                            Id,
                            ConditionGroupId,
                            ItemsFBShomareh,
                            LevelNumber,
                            true,
                            ObjRelated
                        );
                    }
                    else {
                        CheckedOnChange(
                            object,
                            Id,
                            ConditionGroupId,
                            ItemsFBShomareh,
                            LevelNumber,
                            false,
                            null
                        );
                    }
                }
                else {

                    Status = false;

                    if (ItemRelated.length != 0) {

                        // بررسی وجود علامت & در حداقل یکی از مقادیر
                        var hasAnd = ItemRelated.some(function (item) {
                            return String(item).includes('&');
                        });

                        // اگر & وجود داشته باشد، همه باید تیک داشته باشند
                        // در غیر این صورت، تیک داشتن یکی کافی است
                        if (hasAnd) {
                            Status = ItemRelated.every(function (item) {

                                var currentId1 = String(item).split('&');

                                return $('#CK' + currentId1[0]).prop('checked') === true;
                            });
                        }
                        else {
                            Status = ItemRelated.some(function (item) {

                                var currentId1 = String(item).split('&');

                                return $('#CK' + currentId1[0]).prop('checked') === true;
                            });
                        }

                        if (Status) {

                            CheckedOnChange(
                                object,
                                Id,
                                ConditionGroupId,
                                ItemsFBShomareh,
                                LevelNumber,
                                false,
                                null
                            );

                        }
                        else {

                            object.removeAttr('disabled');

                            var id = $(object).attr('id');
                            $('#loading_' + id).remove();

                            object.prop('checked', false);

                            toastr.info(
                                'ابتدا آیتم های پیش نیاز را انتخاب نمایید',
                                'اطلاع'
                            );
                        }
                    }
                    else {

                        CheckedOnChange(
                            object,
                            Id,
                            ConditionGroupId,
                            ItemsFBShomareh,
                            LevelNumber,
                            false,
                            null
                        );
                    }
                }

























                // Status = false;
                // if (response.length != 0) {

                //     for (var i = 0; i < response.length; i++) {

                //         debugger;

                //         currentId = response[i];
                //         currentId1 = String(currentId).split('&');

                //         currentStatus = $('#CK' + currentId1[0]).prop('checked');
                //         ///بررسی اینکه کلیه شروطی که این شرط بدانها وابسته است آیا دارای تیک هستند یا خیر
                //         if (currentStatus) {
                //             Status = true;
                //             break;
                //         }
                //     }

                //     //در صورتی یکی از شروطی که این شرط بدانها وابسته است دارای تیک بود
                //     //پس این شرط نیز اعمال میشود 
                //     if (Status) {
                //         CheckedOnChange(object, Id, ConditionGroupId, ItemsFBShomareh, LevelNumber);
                //     }
                //     else {
                //         object.removeAttr('disabled');

                //         var id = $(object).attr('id');
                //         $('#loading_' + id).remove();

                //         object.attr('checked', false);
                //         toastr.info('ابتدا آیتم های پیش نیاز را انتخاب نمایید', 'اطلاع');

                //     }
                // }
                // else
                // {
                //     CheckedOnChange(object, Id, ConditionGroupId, ItemsFBShomareh, LevelNumber);
                // }
            }
            else
                toastr.error('مشکل در بررسی', 'خطا');
        },
        error: function (response) {
            toastr.error('مشکل در بررسی', 'خطا');
        }
    });
}


function CheckedOnChange(object, Id, ConditionGroupId, ItemsFBShomareh, LevelNumber,hasRelated,ObjRelated) {

    debugger;

    if (object.is(':checked')) {
        $('#FormShow').find('#CK' + Id).attr('checked', true);
        blnFindText = false;
        obj = $('#FormShow').find('#txtMeghdar' + Id);
        if (obj.length != 0) {
            if (!$.isNumeric(obj.val()))
                toastr.info('مقدار وارد شده نامعتبر میباشد', 'اطلاع');
            else {
                ConfirmItemsCondition(object, Id, ConditionGroupId, LevelNumber);
            }
        }
        else {
            ConfirmItemsCondition(object, Id, ConditionGroupId, LevelNumber);
        }
    }
    else {

        object.removeAttr('disabled');

        var id = $(object).attr('id');
        $('#loading_' + id).remove();

        deleteItemsCondition(Id, ConditionGroupId, ItemsFBShomareh, LevelNumber, hasRelated, ObjRelated);

    }
}

function ConfirmItemsCondition(object, Id, ConditionGroupId, LevelNumber) {

    Meghdar = 0;

    RBCode = '';

    IdSplit = Id.split('_');
    RBCode += IdSplit[0];

    txtMeghdarId = "txtMeghdar" + IdSplit[0];
    AllText = $('[id^=' + txtMeghdarId + ']');

    AllText.each(function () {
        Meghdar = $(this).val();
        RBCode += '_' + Meghdar;
    });

    RBCode += ',';

    OperationId = $('#HDFOperationId').val();
    BarAvordUserId = $('#HDFBarAvordUserID').val();
    NoeFBId = parseInt($('#HDFNoeFB').val());
    var vardata = new Object();
    vardata.strRBCode = RBCode;
    vardata.OperationId = OperationId;
    vardata.BarAvordUserId = BarAvordUserId;
    vardata.ConditionGroupId = ConditionGroupId;
    vardata.LevelNumber = LevelNumber;
    vardata.NoeFBId = NoeFBId;

    $.ajax({
        type: "POST",
        url: '/ItemsAddingToFB/SaveItemsAddingToFB',
        dataType: "json",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            object.removeAttr('disabled');

            var id = $(object).attr('id');
            $('#loading_' + id).remove();

            var info = data.split('_');
            if (info[0] == "OK") {
                toastr.success('اطلاعات انتخاب شده بدرستی ثبت گردید', 'موفقیت');

                AllText.each(function () {
                    $(this).removeClass('blinking');
                });
                GetAndShowAddItems(Id, ConditionGroupId);
                GetRizMetreWithFBId(LevelNumber);

                object.parent().parent().find('input[type="checkbox"]').prop('checked', true);

                AllChecks = object.closest('div[id^="divConditionGroup"]')
                    .first()
                    .find('input[type="radio"]');

                AllChecks.each(function (iii) {
                    let tempObjId = object[0].id;
                    let match1 = tempObjId.match(/\d+/);
                    let objId = match1 ? match1[0] : null;

                    let tempId = AllChecks[iii].id;
                    let match = tempId.match(/\d+/);
                    let currentId = match ? match[0] : null;

                    if (currentId != objId) {

                    }
                    else {
                        $(this).prop('checked', true);
                    }
                });


                object.closest('div[id^="divConditionGroup"]')
                    .prevAll('div')
                    .first()
                    .find('input[type="checkbox"]')
                    .prop('checked', true);


                textMeghdar = object.closest('div[id^="divConditionGroup"]')
                    .first()
                    .find('input[type="number"]');

                textMeghdar.each(function (ii) {

                    let tempMeghdarId = textMeghdar[ii].id;
                    let match1 = tempMeghdarId.match(/\d+/);
                    let textMeghdarId = match1 ? match1[0] : null;

                    let tempObjId = object[0].id;
                    let match2 = tempObjId.match(/\d+/);
                    let objId = match2 ? match2[0] : null;

                    if (textMeghdarId != objId) {

                        textMeghdar.removeClass('blinking');

                        axa = $(this).attr('defaultvalue');

                        if (axa === undefined) {
                            axa = null;
                        }
                        $(this).prop('value', axa);

                    }
                });


                IdSplit = Id.split('_');
                $('#divShowRizMetre' + IdSplit[0]).slideDown(500);

            }
            else if (info[0] == "NOK") {
                toastr.error(info[1], 'عدد وارده بایستی در بازه تعریف شده باشد');
                object.attr('checked', false);
            }
            else if (info[0] == "NOOK") {
                toastr.error(info[1]);
                object.attr('checked', false);
            }
            else if (info[0] == "CI") {
                toastr.warning('مقدار را در محدوده متعارف وارد نمایید', 'توجه');
                object.attr('checked', false);

                AllText.each(function () {
                    $(this).addClass('blinking');
                });
                object.focus().select();

                $('#divShowRizMetre' + Id).slideUp();

                object.closest('div[id^="divConditionGroup"]')
                    .prevAll('div')
                    .first()
                    .find('input[type="checkbox"]')
                    .prop('checked', false);

                checkbox = object.parent().find('input[type="checkbox"]')
                if (checkbox != undefined) {
                    checkbox.prop('checked', false);
                }
                else
                    object.parent().parent().find('input[type="checkbox"]').prop('checked', false);

                AllInputs = object.closest('div[id^="divConditionGroup"]').first().find('input[type="number"]');

                if (AllInputs.length > 0) {
                    AllInputs.each(function () {
                        $(this).prop('value', $(this).attr('defaultvalue'));
                    });
                }
                else {
                    object.prop('value', object.attr('defaultvalue'));
                }

            }
        },
        error: function (msg) {
            var id = $(object).attr('id');
            $('#loading_' + id).remove();

            object.closest('div[id^="divConditionGroup"]')
                .prevAll('div')
                .first()
                .find('input[type="checkbox"]')
                .prop('checked', false);

            checkbox = object.parent().find('input[type="checkbox"]')
            if (checkbox != undefined) {
                checkbox.prop('checked', false);
            }
            else
                object.parent().parent().find('input[type="checkbox"]').prop('checked', false);

            AllInputs = object.closest('div[id^="divConditionGroup"]').first().find('input[type="number"]');

            if (AllInputs.length > 0) {
                AllInputs.each(function () {
                    $(this).prop('value', $(this).attr('defaultvalue'));
                });
            }
            else {
                object.prop('value', object.attr('defaultvalue'));
            }

            var $obj = $(object);

            // غیرفعال کردن
            $obj.removeAttr('disabled');

            toastr.error('خطا در درج اطلاعات', 'خطا');
        }
    });
}

function textMeghdarOnChange(object, ConditionGroupId, LevelNumber) {

    //check = false;
    //object.parent().parent().find('input[type=number]').each(function () {
    //    debugger;
    //    defaultValue = $(this).attr('defaultvalue');
    //Value = this.value;

    //if (Value < defaultValue) {
    //    $(this).addClass('blinking');
    //    this.focus();
    //    toastr.error('مقدار وارد شده در محدوده مجاز نمیباشد', 'هشدار');
    //    check = true;
    //    $(this).prop('value', defaultValue)
    //    return false;
    //}
    //else
    //    $(this).removeClass('blinking');
    //});

    //if (check) {
    //    return;
    //}

    debugger;
    var $obj = $(object);
    // غیرفعال کردن
    $obj.prop('disabled', true);
    // اضافه کردن waiting
    var $loading = $('<span>', {
        id: 'loading_' + $obj.attr('id'),
        class: 'my-loading'
    });
    $obj.before($loading);

    Id = object.attr('id').substring(10, object.attr('id').length);

    $('#CK' + Id).prop('checked', true);

    $("div[id^='divShowRizMetre']").not('#divShowRizMetre' + Id).slideUp(500);

    ConfirmItemsCondition(object, Id, ConditionGroupId, LevelNumber);

}
//////////////
function AddRizMetre() {
    $('#aAddRizMetre').click();
    $('#txtSharhRizMetre').val('');
    $('#txtTedad').val(0);
    $('#txtToolRizMetre').val('0.0');
    $('#txtArzRizMetre').val('0.0');
    $('#txtErtefaRizMetre').val('0.0');
    $('#txtErtefaRizMetre').val('0.0');
    $('#txtVaznRizMetre').val('0.0');
    $('#HDFSaveEdit').val('Add');
    var FBId = $('#HDFFBID').val();
    var vardata = new Object();
    vardata.FBId = FBId;
    $.ajax({
        type: "POST",
        url: '/RizMetreUser/GetLastShomarehRizMetreUsers',
        dataType: "json",
        //data: '{FBId:' + FBId + '}',
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var info = data.d.split('_');
            if (info[0] == "OK") {
                $('#FormShow').find('#txtShomarehRizMetre').val(info[1]);
            }
            else
                toastr.info('مشکل در بارگزاری ریزه متره انتخابی', 'اطلاع');
        },
        error: function (msg) {
            toastr.error('مشکل در بارگزاری ریزه متره انتخابی', 'خطا');
        }
    });
}

//function CheckItemsHasConditionManualAdding(OperationId) {
//    $('#HDFOperationId').val(OperationId);
//    BarAvordUserId = $('#HDFBarAvordID').val();
//    $.ajax({
//        type: "POST",
//        url: 'GetInfo.aspx/GetItemsHasConditionForManual',
//        dataType: "json",
//        data: '{OperationId:' + OperationId + ',BarAvordId:' + BarAvordID + '}',
//        contentType: "application/json; charset=utf-8",
//        success: function (data) {
//            var info = data.d.split('_');
//            if (info[0] == "OK") {
//                $('#aItemsCondition').click();
//                $('#TitleItemsCondition').html(' اضافه یا کسر بهای آیتم ' + info[1]);
//                $('#divItemsCondition').html(info[2]);
//                $('#divItemsCondition input[type=text]').change(function () {
//                    id = $(this).attr('id').substring(10, $(this).attr('id').length);
//                    $(this).parent().parent().find($('#CK' + id)).attr('checked', 'checked');
//                });
//            }
//            else
//                toastr.info('مشکل در بارگزاری ریزه متره انتخابی', 'اطلاع');
//        },
//        error: function (msg) {
//            toastr.error('مشکل در بارگزاری ریزه متره انتخابی', 'خطا');
//        }
//    });
//}

function KhakRiziClick(OpId, event) {
    if (event) {
        event.stopPropagation();
        event.preventDefault();
    }
    BarAvordUserID = $('#HDFBarAvordUserID').val();
    KhakRiziWithBarAvordClick(OpId, BarAvordUserID);
}

function GharzehClick(OpId) {
    BarAvordUserID = $('#HDFBarAvordUserID').val();
    GharzehWithBarAvordClick(OpId, BarAvordUserID);
}

function PayKaniClick(OpId, event) {

    if (event) {
        event.stopPropagation();
        event.preventDefault();
    }

    BarAvordUserID = $('#HDFBarAvordUserID').val();
    PayKaniWithBarAvordIdClick(OpId, BarAvordUserID);
}

function spanOnClick(Id, ConditionGroupId) {

    $("div[id^='divShowRizMetre']").not('#divShowRizMetre' + Id).slideUp(500);

    let checkbox = $('#CK' + Id);

    if (checkbox.is(':checked')) {
        $('#divShowRizMetre' + Id).slideToggle(500);
        GetAndShowAddItems(Id, ConditionGroupId);

    } else {
        $('#CK' + Id).click();
        checkbox.prop('checked', true);
    }

}

////////
function OpenConditionDetailsOnly(ConditionGroupId) {
    $('#divConditionGroup' + ConditionGroupId).toggle(1000);
}
function OperationClick(Operation, showAddedRecord, event, TypePayKani) {
    debugger;


    $('#HDFPayKani').val(TypePayKani);
    if (event) {
        event.stopPropagation();
        event.preventDefault();
    }

    if ($("#ula" + Operation).is(":visible")) {
    } else {
        Type = 0;
        $('#HDFOperationId').val(Operation);
        ItemsFBShomareh = $('#HDFItemsFBShomareh').val();
        FBId = $('#HDFFBID').val();
        NoeFBId = parseInt($('#HDFNoeFB').val());
        BarAvordUserId = $('#HDFBarAvordUserID').val();
        Year = $('#HDFYear').val();
        var vardata = new Object();
        vardata.ItemsFBShomareh = ItemsFBShomareh;
        vardata.Operation = Operation;
        vardata.BarAvordUserId = BarAvordUserId;
        vardata.NoeFBId = NoeFBId;
        vardata.Year = Year;
        vardata.LevelNumber = 1;
        vardata.FBId = FBId;
        vardata.NoeFBId = NoeFBId;

        //if (!$('#a' + Operation).hasClass('OpenMenu')) {
            $.ajax({
                type: "POST",
                url: "/Operation_ItemsFB/GetOperation_ItemsFB",
                data: JSON.stringify(vardata),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    debugger;
                    result = response.result;
                    if (result == "OK") {
                        lstItemsFields = response.lstItemsFields;
                        lstItemsFieldEssentialValue = response.lstItemsFieldEssentialValue;
                        lstDefaultMaxMin = response.lstDefaultMaxMin;
                        ItemsFBShomareh = response.itemsFBShomareh;
                        str = response.str;
                        FBId = response.fbId;

                        $('#a' + Operation).addClass('OpenMenu');
                        $('#HDFFBID').val(FBId);
                        //alert($('#HDFFBID').val());
                        $('#HDFItemsFBShomareh').val(ItemsFBShomareh);
                        $('#uldiva' + Operation).html(str);
                        $('#ula' + Operation).show();
                        $('#HDFOperationId').val(Operation);

                        if (showAddedRecord) {
                            AddRizMetreUsers(Operation, FBId, lstItemsFields, lstItemsFieldEssentialValue, lstDefaultMaxMin, 1, ItemsFBShomareh);
                        }

                        GetRizMetreWithFBId(1);
                        //$('#divShowRizMetre').html($('#uldiva' + Operation).html());
                        //$('#aShow').click();
                    }
                    else
                        toastr.error('مشکل در محاسبه جمع کل فصل انتخابی', 'خطا');
                },
                error: function (response) {
                    toastr.error('مشکل در محاسبه جمع کل فصل انتخابی', 'خطا');
                }
            });
        //}
        //else
           // $('#a' + Operation).removeClass('OpenMenu');
    }
}
///////////////
function OperationClickSecondLevel(Operation) {
    Type = 0;
    $('#HDFOperationId').val(Operation);
    ItemsFBShomareh = $('#HDFItemsFBShomareh').val();
    FBId = $('#HDFFBID').val();
    NoeFB = parseInt($('#HDFNoeFB').val());
    BarAvordUserId = $('#HDFBarAvordUserID').val();
    Year = $('#HDFYear').val();
    var vardata = new Object();
    vardata.ItemsFBShomareh = ItemsFBShomareh;
    vardata.Operation = Operation;
    vardata.BarAvordUserId = BarAvordUserId;
    vardata.NoeFB = parseInt(NoeFB);
    vardata.Year = Year;
    vardata.LevelNumber = 2;
    if (!$('#PopupViewNextRutinTreeView').find('#a' + Operation).hasClass('OpenMenu')) {
        $('#PopupViewNextRutinTreeView').find('.OpenMenu').removeClass('OpenMenu');

        $('#PopupViewNextRutinTreeView').find('[id^="ula"]').hide();
        $.ajax({
            type: "POST",
            url: "/Operation_ItemsFB/GetOperation_ItemsFB",
            //data: '{ItemsFBShomareh:' + "'" + ItemsFBShomareh + "'" +
            //    ',Operation :' + Operation + ',BarAvordUserId:' + "'" + BarAvordUserID + "'"
            //    + ',NoeFB:' + "'eec10c4b-5452-4677-a22b-ab5ea9b4e3f0'" + ',Year:'+"'1397'"+'}',
            data: JSON.stringify(vardata),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                info = response.split('_');
                if (info[0] == "OK") {
                    $('#PopupViewNextRutinTreeView').find('#a' + Operation).addClass('OpenMenu');
                    $('#HDFFBID').val(info[1]);
                    $('#PopupViewNextRutinTreeView').find('#uldiva' + Operation).html(info[2]);
                    $('#PopupViewNextRutinTreeView').find('#ula' + Operation).show();
                    //$('#divShowRizMetre').html($('#uldiva' + Operation).html());
                    //$('#aShow').click();
                }
                else
                    toastr.error('مشکل در محاسبه جمع کل فصل انتخابی', 'خطا');
            },
            error: function (response) {
                toastr.error('مشکل در محاسبه جمع کل فصل انتخابی', 'خطا');
            }
        });
    }
    else {
        $('#PopupViewNextRutinTreeView').find('#a' + Operation).removeClass('OpenMenu');
        $('#PopupViewNextRutinTreeView').find('#ula' + Operation).hide();
    }
}

function NOperationClick(Operation, LevelNumber) {
    Type = $('#HDFOperationHasAddedOperations').val();
    ItemsFBShomareh = $('#HDFItemsFBShomareh').val();
    if (!$('#PopupViewNextRutin').find('#na' + Operation).hasClass('OpenMenu')) {
        BarAvordUserId = $('#HDFBarAvordUserID').val();
        var vardata = new Object();
        vardata.ItemsFBShomareh = ItemsFBShomareh;
        vardata.Operation = Operation;
        vardata.BarAvordUserId = BarAvordUserId;
        vardata.Type = Type;
        vardata.Year = $('#HDFYear').val();
        vardata.NoeFB = parseInt($('#HDFNoeFB').val());
        vardata.LevelNumber = LevelNumber;
        $.ajax({
            type: "POST",
            url: "/Operation_ItemsFB/GetNOperation_ItemsFB",
            //data: '{ItemsFBShomareh:' + "'" + ItemsFBShomareh
            //    + "'" + ',Operation :' + Operation
            //    + ',BarAvordUserId:' + BarAvordUserId + ',Type:' + Type + '}',
            data: JSON.stringify(vardata),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                info = response.split('_');
                if (info[0] == "OK") {

                    if (LevelNumber == 1) {
                        $('#PopupViewNextRutin').find("div[id^='nulna']").slideUp(1000);
                        $('#PopupViewNextRutin').find('.OpenMenu').removeClass('OpenMenu');
                        $('#PopupViewNextRutin').find('#na' + Operation).addClass('OpenMenu');
                        $('#HDFFBID').val(info[1]);
                        $('#PopupViewNextRutin').find('#nuldivna' + Operation).html(info[2]);
                        $('#PopupViewNextRutin').find('#nulna' + Operation).toggle(1000);
                        if (info[3] == "True") {
                            $('#btnAddRizMetre').addClass('displayNone');
                        }
                        else
                            $('#btnAddRizMetre').removeClass('displayNone');
                    }
                    else {
                        $('#PopupViewNextRutinTreeView').find("div[id^='nulna']").slideUp(1000);
                        $('#PopupViewNextRutinTreeView').find('.OpenMenu').removeClass('OpenMenu');
                        $('#PopupViewNextRutinTreeView').find('#na' + Operation).addClass('OpenMenu');
                        $('#HDFFBID').val(info[1]);
                        $('#PopupViewNextRutinTreeView').find('#nuldivna' + Operation).html(info[2]);
                        $('#PopupViewNextRutinTreeView').find('#nulna' + Operation).toggle(1000);
                        if (info[3] == "True") {
                            $('#btnAddRizMetre').addClass('displayNone');
                        }
                        else
                            $('#btnAddRizMetre').removeClass('displayNone');
                    }
                }
                else
                    toastr.error('مشکل در محاسبه جمع کل فصل انتخابی', 'خطا');
            },
            error: function (response) {
                toastr.error('مشکل در محاسبه جمع کل فصل انتخابی', 'خطا');
            }
        });
    }
    else {
        $('#PopupViewNextRutin').find('#na' + Operation).removeClass('OpenMenu');
        $('#PopupViewNextRutin').find('#nulna' + Operation).toggle(1000);
    }
}

function spanAddedItemsClick(Id) {
    $('#PopupViewNextRutin').find('#na' + Id).click();
}
///////////

function spanAddedItemsClickSecondLevel(Id) {
    $('#PopupViewNextRutinSecondLevel').find('#na' + Id).click();
}
///////////
function KhakBardariMashinClick(OpId, event) {
    if (event) {
        event.stopPropagation();
        event.preventDefault();
    }
    //clearTimeout(timerAddPol);
    BarAvordUserID = $('#HDFBarAvordUserID').val();
    KhakBardariMashinWithBarAvordIdClick(OpId, BarAvordUserID);
}
//////////
function ShowAndTickAllEzafeBahaAndLakeGiri(object, ItemsFBShomareh, ConditionGroupId, LevelNumber, Id) {

    if (!object.is(':checked')) {
        //    str = '';
        //    if (LevelNumber == 1) {
        //        debugger;

        //        $('#spanFieldShomarehName').html('شماره');
        //        $('#Grid' + ItemsFBShomareh).find("input[id^='CKEzafeBaha']").each(function () {
        //            debugger;

        //            $('#' + $(this).attr('id')).addClass('displayNone');
        //            if ($(this).is(':checked')) {
        //                len = ConditionGroupId.length;
        //                Id = $(this).attr('id');
        //                IdOnly = Id.substring(11, (Id.length - len));
        //                if (Id.substring((Id.length - len), Id.length) == ConditionGroupId) {
        //                    Tool = $('#txtTool' + IdOnly).val();
        //                    Ertefa = $('#txtErtefa' + IdOnly).val();
        //                    Arz = $('#txtArz' + IdOnly).val();
        //                    str += IdOnly + ',' + Tool + ',' + Ertefa + ',' + Arz + '_';
        //                }
        //            }
        //        });
        //    }
        //    else if (LevelNumber == 2) {
        //        $('#PopupViewNextRutinTreeView').find('#spanFieldShomarehName').html('شماره');
        //        $('#PopupViewNextRutinTreeView').find('#Grid' + ItemsFBShomareh).find("input[id^='CKEzafeBaha']").each(function () {
        //            $('#' + $(this).attr('id')).addClass('displayNone');
        //            if ($(this).is(':checked')) {
        //                len = ConditionGroupId.length;
        //                Id = $(this).attr('id');
        //                IdOnly = Id.substring(11, (Id.length - len));
        //                if (Id.substring((Id.length - len), Id.length) == ConditionGroupId) {
        //                    Tool = $('#PopupViewNextRutinTreeView').find('#txtTool' + IdOnly).val();
        //                    Ertefa = $('#PopupViewNextRutinTreeView').find('#txtErtefa' + IdOnly).val();
        //                    Arz = $('#PopupViewNextRutinTreeView').find('#txtArz' + IdOnly).val();
        //                    str += IdOnly + ',' + Tool + ',' + Ertefa + ',' + Arz + '_';
        //                }
        //            }
        //        });
        //    }

        BarAvordUserId = $('#HDFBarAvordUserID').val();
        FBId = $('#HDFFBID').val();
        var vardata = new Object();
        vardata.ItemShomareh = ItemsFBShomareh;
        vardata.BarAvordId = BarAvordUserId;
        vardata.LevelNumber = LevelNumber;
        vardata.Year = Year;
        vardata.FBId = FBId;
        vardata.ConditionGroupId = ConditionGroupId;
        vardata.RBCode = Id;

        $.ajax({
            type: "POST",
            url: '/RizMetreUser/DeleteEzafeBahaAndLakeGiriRizMetreAll',
            dataType: "json",
            data: JSON.stringify(vardata),
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                info = data.split('_');
                if (info[0] == "OK") {
                    //GetRizMetreWithFBId(1);
                    //GetRizMetreUsers(LevelNumber);
                    $('#divShowRizMetre' + Id).html('');
                    $('#divShowRizMetre' + Id).slideUp(500);
                    toastr.success('حذف بدرستی صورت گرفت', 'موفقیت');
                }
            },
            error: function (msg) {
                toastr.error('مشکل در حذف اطلاعات', 'خطا');
            }
        });
    }
    else {
        //    debugger;

        //    $('#spanFieldShomarehName').html('انتخاب');
        //    str = '';
        //    if (LevelNumber == 1) {
        //        $('#Grid' + ItemsFBShomareh).find("input[id^='CKEzafeBaha']").each(function () {
        //            debugger;

        //            $('#' + $(this).attr('id')).removeClass('displayNone');
        //            $(this).prop('checked', true);
        //            len = ConditionGroupId.length;
        //            Id = $(this).attr('id');
        //            IdOnly = Id.substring(11, (Id.length - len));
        //            if (Id.substring((Id.length - len), Id.length) == ConditionGroupId) {
        //                Tool = $('#txtTool' + IdOnly).val();
        //                Ertefa = $('#txtErtefa' + IdOnly).val();
        //                Arz = $('#txtArz' + IdOnly).val();
        //                str += IdOnly + ',' + Tool + ',' + Ertefa + ',' + Arz + '_';
        //            }
        //        });
        //    }
        //    else if (LevelNumber == 2) {
        //        $('#PopupViewNextRutinTreeView').find('#Grid' + ItemsFBShomareh).find("input[id^='CKEzafeBaha']").each(function () {
        //            $('#PopupViewNextRutinTreeView').find('#' + $(this).attr('id')).removeClass('displayNone');
        //            $(this).prop('checked', true);
        //            len = ConditionGroupId.length;
        //            Id = $(this).attr('id');
        //            IdOnly = Id.substring(11, (Id.length - len));
        //            if (Id.substring((Id.length - len), Id.length) == ConditionGroupId) {
        //                Tool = $('#PopupViewNextRutinTreeView').find('#txtTool' + IdOnly).val();
        //                Ertefa = $('#PopupViewNextRutinTreeView').find('#txtErtefa' + IdOnly).val();
        //                Arz = $('#PopupViewNextRutinTreeView').find('#txtArz' + IdOnly).val();
        //                str += IdOnly + ',' + Tool + ',' + Ertefa + ',' + Arz + '_';
        //            }
        //        });
        //    }

        Year = $('#HDFYear').val();
        FBId = $('#HDFFBID').val();
        NoeFBId = parseInt($('#HDFNoeFB').val());

        BarAvordUserId = $('#HDFBarAvordUserID').val();
        var vardata = new Object();
        //vardata.Param = str;
        vardata.ItemShomareh = ItemsFBShomareh;
        vardata.BarAvordId = BarAvordUserId;
        vardata.LevelNumber = LevelNumber;
        vardata.Year = Year;
        vardata.FBId = FBId;
        vardata.ConditionGroupId = ConditionGroupId;
        vardata.RBCode = Id;
        vardata.NoeFBId = NoeFBId;

        $.ajax({
            type: "POST",
            url: '/RizMetreUser/SaveEzafeBahaAndLakeGiriRizMetreAll',
            dataType: "json",
            data: JSON.stringify(vardata),
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                info = data.split('_');
                if (info[0] == "OK") {
                    if ($('#divShowRizMetre' + Id).is(':visible')) {
                        $('#divShowRizMetre' + Id).slideToggle(500);
                    }
                    else {
                        GetAndShowAddItems(Id, ConditionGroupId);
                        $('#divShowRizMetre' + Id).slideToggle(500);
                    }

                    //GetRizMetreUsers(LevelNumber);
                    toastr.success('درج بدرستی صورت گرفت', 'موفقیت');
                }
                else
                    toastr.warning('خطا در انجام عملیات حذف', 'خطا');
            },
            error: function (msg) {
                toastr.error('خطا در انجام عملیات حذف', 'خطا');
            }
        });
    }
}

function ShowEzafeBahaAndLakeGiriOnly(object, ItemsFBShomareh, ConditionGroupId, Id) {

    //$('#spanFieldShomarehName').html('انتخاب');
    //$('#Grid' + ItemsFBShomareh).find("input[id^='CKEzafeBaha']").each(function () {
    //    len = ConditionGroupId.length;
    //    Id = $(this).attr('id');
    //    if (Id.substring((Id.length - len), Id.length) == ConditionGroupId)
    //        $('#' + Id).removeClass('displayNone');
    //});

    //if ($('#divShowRizMetre' + Id).is(':visible')) {
    //    $('#divShowRizMetre' + Id).slideToggle(500);
    //}
    //else {
    //    GetAndShowAddItems(Id, ConditionGroupId);
    //    $('#divShowRizMetre' + Id).slideToggle(500);
    //}


    $("div[id^='divShowRizMetre']").not('#divShowRizMetre' + Id).slideUp(500);

    let checkbox = $(object).closest('div').find('input[type=checkbox]');

    if (checkbox.is(':checked')) {
        {
            GetAndShowAddItems(Id, ConditionGroupId);
            $('#divShowRizMetre' + Id).slideToggle(500);
        }
    } else {
        //$('#CK' + Id).click();
        checkbox.trigger('click');
        checkbox.prop('checked', true);
    }
}

function SelectEzafeBahaOrLakeGiriRecords(Id, ItemShomareh, LevelNumber) {


    var Tool, Ertefa, Arz;
    BarAvordUserId = $('#HDFBarAvordUserID').val();

    if (LevelNumber == 1) {
        Tool = $('#txtTool' + Id).val();
        Ertefa = $('#txtErtefa' + Id).val();
        Arz = $('#txtArz' + Id).val();
    }
    else if (LevelNumber == 2) {
        Tool = $('#PopupViewNextRutinTreeView').find('#txtTool' + Id).val();
        Ertefa = $('#PopupViewNextRutinTreeView').find('#txtErtefa' + Id).val();
        Arz = $('#PopupViewNextRutinTreeView').find('#txtArz' + Id).val();
    }
    var vardata = new Object();
    vardata.Tool = Tool;
    vardata.Ertefa = Ertefa;
    vardata.Arz = Arz;
    vardata.ItemShomareh = ItemShomareh;
    vardata.RizMetreId = Id;
    vardata.BarAvordId = BarAvordUserId;
    vardata.LevelNumber = LevelNumber;
    vardata.Year = $('#HDFYear').val();
    vardata.NoeFB = parseInt($('#HDFNoeFB').val());

    $.ajax({
        type: "POST",
        url: '/RizMetreUser/SaveEzafeBahaAndLakeGiriRizMetre',
        dataType: "json",
        //data: '{Tool:' + "'" + Tool + "'" + ',Ertefa:' + "'" + Ertefa + "'" + ',Arz:' + "'" + Arz + "'"
        //    + ',ItemShomareh:' + "'" + ItemShomareh + "'" + ',RizMetreId:' + Id + ',BarAvordId:' + BarAvordId + '}',
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            info = data.split('_');
            if (info[0] == "OK") {
                //GetRizMetreUsers(LevelNumber);
                toastr.success(info[1], 'موفقیت');
            }
        },
        error: function (msg) {
            toastr.error('خطا در انجام عملیات لکه گیری', 'خطا');
        }
    });
}


function CheckInfoRizMetre() {
    var Sharh = $('#txtSharhRizMetre').val();
    var Tedad = $('#txtTedad').val();
    var Tool = $('#txtToolRizMetre').val();
    var Arz = $('#txtArzRizMetre').val();
    var Ertefa = $('#txtErtefaRizMetre').val();
    var Vazn = $('#txtVaznRizMetre').val();
    if (Sharh == '') return 1;
    if (!($.isNumeric(Tedad))) return 2;
    if (!($.isNumeric(Tool))) return 3;
    if (!($.isNumeric(Arz))) return 4;
    if (!($.isNumeric(Ertefa))) return 5;
    if (!($.isNumeric(Vazn))) return 6;
    return 0;
}
function AddRizMetreUsers(OperationId, FBId, Array, lstItemsFieldEssentialValue, lstDefaultMaxMin, LevelNumber, ItemsFBShomareh) {

    debugger;
    ArraySplit = Array.split(',');
    EssentialSplit = lstItemsFieldEssentialValue.split(',');

    $('#HDFOperationId').val(OperationId);
    $('#HDFFBID').val(FBId);
    $('#HDFIsFromAddedOperation').val('false');
    $('.NoRizMetre').addClass('displayNone');
    //var str = '';
    //str += "<div id=\"divNewRow\" class=\"row styleRowTable\"><div class=\"col-md-1\"><i class=\"fa fa-plus SaveRMUStyle\"></i></div>";
    //str += "<div class=\"col-md-2\"><input type=\"text\"  class=\"form-control spanStyleMitraSmall\" id=\"txtSharh" + OperationId + "\ value=\"\"/></div>";
    //str += "<div class=\"col-md-1\"><input type=\"text\"" + (ArraySplit[0] != "True" ? "disabled=\"disabled\" class=\"form-control spanStyleMitraSmall\"" : " class=\"form-control spanStyleMitraSmall HasEnteringValue\"") + " id=\"txtTedad" + OperationId + "\ value=\"\"/></div>";
    //str += " <div class=\"col-md-1\"><input type=\"text\"" + (ArraySplit[1] != "True" ? "disabled=\"disabled\" class=\"form-control spanStyleMitraSmall\"" : " class=\"form-control spanStyleMitraSmall HasEnteringValue\"") + " id=\"txtTool" + OperationId + "\ value=\"\"/></div>";
    //str += "<div class=\"col-md-1\"><input type=\"text\"" + (ArraySplit[2] != "True" ? "disabled=\"disabled\" class=\"form-control spanStyleMitraSmall\"" : " class=\"form-control spanStyleMitraSmall HasEnteringValue\"") + " id=\"txtArz" + OperationId + "\ value=\"\"/></div>";
    //str += "<div class=\"col-md-1\"><input type=\"text\"" + (ArraySplit[3] != "True" ? "disabled=\"disabled\" class=\"form-control spanStyleMitraSmall\"" : " class=\"form-control spanStyleMitraSmall HasEnteringValue\"") + " id=\"txtErtefa" + OperationId + "\ value=\"\"/></div>";
    //str += "<div class=\"col-md-1\"><input type=\"text\"" + (ArraySplit[4] != "True" ? "disabled=\"disabled\" class=\"form-control spanStyleMitraSmall\"" : " class=\"form-control spanStyleMitraSmall HasEnteringValue\"") + " id=\"txtVazn" + OperationId + "\ value=\"\"/></div>";
    //str += "<div class=\"col-md-1\">0</div>";
    //str += "<div class=\"col-md-2\"><input type=\"text\" class=\"form-control spanStyleMitraSmall\" id=\"txtDes" + OperationId + "\ value=\"\"/></div>";
    //str += "<div class=\"col-md-1\"><button type=\"button\" onclick=\"SaveRMUClick($(this),'" + FBId + "','false','" + OperationId + "'," + LevelNumber + ")\" class=\"ButtonRowsSaveStyle\"><i id=\"iSave\" class=\"fa fa-save SaveRMUStyle\"></i></button></div></div>";

    let Essential0 = EssentialSplit[0] == "True" ? " Essential " : "";
    let Essential1 = EssentialSplit[1] == "True" ? " Essential " : "";
    let Essential2 = EssentialSplit[2] == "True" ? " Essential " : "";
    let Essential3 = EssentialSplit[3] == "True" ? " Essential " : "";
    let Essential4 = EssentialSplit[4] == "True" ? " Essential " : "";

    let DefaultTedad = "";
    let MinTedad = "";
    let MaxTedad = "";

    let DefaultTool = "";
    let MinTool = "";
    let MaxTool = "";

    let DefaultArz = "";
    let MinArz = "";
    let MaxArz = "";

    let DefaultErtefa = "";
    let MinErtefa = "";
    let MaxErtefa = "";

    let DefaultVazn = "";
    let MinVazn = "";
    let MaxVazn = "";

    let DefaultMaxMinSplit = (lstDefaultMaxMin || "").split(",");

    // تابع برای جلوگیری از خطا
    function GetDefaultMinMax(index) {

        if (index >= DefaultMaxMinSplit.length)
            return ["", "", ""];

        let value = (DefaultMaxMinSplit[index] || "").trim();

        if (value === "")
            return ["", "", ""];

        let split = value.split("_");

        return [
            split[0] || "",
            split[1] || "",
            split[2] || ""
        ];
    }

    // تعداد
    let Tedad = GetDefaultMinMax(0);
    DefaultTedad = RemoveExtraDecimalZeros(Tedad[0]);
    MinTedad = RemoveExtraDecimalZeros(Tedad[1]);
    MaxTedad = RemoveExtraDecimalZeros(Tedad[2]);

    // طول
    let Tool = GetDefaultMinMax(1);
    DefaultTool = RemoveExtraDecimalZeros(Tool[0]);
    MinTool = RemoveExtraDecimalZeros(Tool[1]);
    MaxTool = RemoveExtraDecimalZeros(Tool[2]);

    // عرض
    let Arz = GetDefaultMinMax(2);
    DefaultArz = RemoveExtraDecimalZeros(Arz[0]);
    MinArz = RemoveExtraDecimalZeros(Arz[1]);
    MaxArz = RemoveExtraDecimalZeros(Arz[2]);

    // ارتفاع
    let Ertefa = GetDefaultMinMax(3);
    DefaultErtefa = RemoveExtraDecimalZeros(Ertefa[0]);
    MinErtefa = RemoveExtraDecimalZeros(Ertefa[1]);
    MaxErtefa = RemoveExtraDecimalZeros(Ertefa[2]);

    // وزن
    let Vazn = GetDefaultMinMax(4);
    DefaultVazn = RemoveExtraDecimalZeros(Vazn[0]);
    MinVazn = RemoveExtraDecimalZeros(Vazn[1]);
    MaxVazn = RemoveExtraDecimalZeros(Vazn[2]);



    let str = `
<div id="divNewRow" class="row styleRowTable">
    <div class="col-md-1">
        <i class="fa fa-plus SaveRMUStyle"></i>
    </div>

    <div class="col-md-2">
        <input type="text" class="form-control spanStyleMitraSmall"
               id="txtSharh${OperationId}" value="" />
    </div>

    <div class="col-md-1">
        <input type="text"
            ${ArraySplit[0] != "True"
            ? `disabled="disabled" class="form-control spanStyleMitraSmall ${Essential0}"`
            : `class="form-control spanStyleMitraSmall HasEnteringValue ${Essential0}"`}
            id="txtTedad${OperationId}"
            value="${DefaultTedad}" 
            data-min="${MinTedad}"
            data-max="${MaxTedad}" />
    </div>

    <div class="col-md-1">
        <input type="text"
            ${ArraySplit[1] != "True"
            ? `disabled="disabled" class="form-control spanStyleMitraSmall ${Essential1}"`
            : `class="form-control spanStyleMitraSmall HasEnteringValue ${Essential1}"`}
            id="txtTool${OperationId}"
            value="${DefaultTool}"
            data-min="${MinTool}"
            data-max="${MaxTool}"/>
    </div>

    <div class="col-md-1">
        <input type="text"
            ${ArraySplit[2] != "True"
            ? `disabled="disabled" class="form-control spanStyleMitraSmall ${Essential2}"`
            : `class="form-control spanStyleMitraSmall HasEnteringValue ${Essential2}"`}
            id="txtArz${OperationId}"
            value="${DefaultArz}"
            data-min="${MinArz}"
            data-max="${MaxArz}"/>
    </div>

    <div class="col-md-1">
        <input type="text"
            ${ArraySplit[3] != "True"
            ? `disabled="disabled" class="form-control spanStyleMitraSmall ${Essential3}"`
            : `class="form-control spanStyleMitraSmall HasEnteringValue ${Essential3}"`}
            id="txtErtefa${OperationId}"
            value="${DefaultErtefa}"
            data-min="${MinErtefa}"
            data-max="${MaxErtefa}"/>
    </div>

    <div class="col-md-1">
        <input type="text"
            ${ArraySplit[4] != "True"
            ? `disabled="disabled" class="form-control spanStyleMitraSmall ${Essential4}"`
            : `class="form-control spanStyleMitraSmall HasEnteringValue ${Essential4}"`}
            id="txtVazn${OperationId}"
            value="${DefaultVazn}"
            data-min="${MinVazn}"
            data-max="${MaxVazn}"/>
    </div>

    <div class="col-md-1">0</div>

    <div class="col-md-2">
        <input type="text" class="form-control spanStyleMitraSmall"
               id="txtDes${OperationId}" value="" />
    </div>

    <div class="col-md-1">
        <button type="button"
            onclick="SaveRMUClick($(this),'${FBId}','false','${OperationId}',${LevelNumber})"
            class="ButtonRowsSaveStyle">
            <i id="iSave" class="fa fa-save SaveRMUStyle"></i>
        </button>
    </div>
</div>
`;

    debugger;
    NoeFBId = parseInt($('#HDFNoeFB').val());

    if (LevelNumber == 1) {
        $('#Grid' + NoeFBId + "_" + ItemsFBShomareh).append(str);


        $('#uldiva' + OperationId + ' #divNewRow').find('input[type=text]').each(function () {

            if ($(this).attr('id').startsWith('txtSharh')) {
                $(this).focus();
            }

            $(this).addClass('TextEdit');

            $(this).click(function () {
                if (!($(this).is(':focus')))
                    $(this).select();
            });
            $(this).focus(function () {
                $(this).select();
            });

            $(this).on("keypress", function (e) {
                /* ENTER PRESSED */
                if (e.keyCode == 13) {
                    /* FOCUS ELEMENT */
                    var inputs = $(this).parent().parent().find("input[Type=text],button");
                    var idx = inputs.index(this);
                    if (idx == inputs.length - 1) {
                        inputs[0].focus();
                        inputs[0].select();
                    } else {
                        while (inputs[idx + 1].disabled == true) {
                            idx++;
                        }
                        inputs[idx + 1].focus(); //  handles submit buttons
                        inputs[idx + 1].select();
                    }
                    return false;
                }
            });
        })
    }
    else if (LevelNumber == 2) {
        $('#PopupViewNextRutinTreeView').find('#Grid' + NoeFBId + "_" + ItemsFBShomareh).append(str);

        $('#PopupViewNextRutinTreeView').find('#uldiva' + OperationId + ' #divNewRow').find('input[type=text]').each(function () {
            $(this).click(function () {
                if (!($(this).is(':focus')))
                    $(this).select();
            });
            $(this).focus(function () {
                $(this).select();
            });

            $(this).on("keypress", function (e) {
                /* ENTER PRESSED */
                if (e.keyCode == 13) {
                    /* FOCUS ELEMENT */
                    var inputs = $(this).parent().parent().find("input[Type=text],button");
                    var idx = inputs.index(this);
                    if (idx == inputs.length - 1) {
                        inputs[0].focus();
                        inputs[0].select();
                    } else {
                        while (inputs[idx + 1].disabled == true) {
                            idx++;
                        }
                        inputs[idx + 1].focus(); //  handles submit buttons
                        inputs[idx + 1].select();
                    }
                    return false;
                }
            });
        })
    }
}



function ValidateMinMax(inputId, fieldName) {

    let input = $("#" + inputId);

    if (input.length === 0 || input.prop("disabled"))
        return true;

    let value = input.val()?.toString().trim();

    // اگر خالی باشد، اینجا بررسی نمی‌کنیم
    if (value === "")
        return true;

    let min = input.data("min");
    let max = input.data("max");

    // اگر Min یا Max تعریف نشده باشد
    if (min === undefined || min === "" || min === null)
        min = null;

    if (max === undefined || max === "" || max === null)
        max = null;

    let number = parseFloat(value);

    // عدد معتبر نیست
    if (isNaN(number)) {
        toastr.info(`مقدار وارد شده برای ${fieldName} معتبر نیست.`);
        input.focus();
        return false;
    }

    // بررسی حداقل
    if (min !== null && number < parseFloat(min)) {
        toastr.info(`مقدار ${fieldName} نباید کمتر از ${min} باشد.`);
        input.focus();
        return false;
    }

    // بررسی حداکثر
    if (max !== null && number > parseFloat(max)) {
        toastr.info(`مقدار ${fieldName} نباید بیشتر از ${max} باشد.`);
        input.focus();
        return false;
    }

    return true;
}



function SaveRMUClick(object, FBId, IsFromAddedOperation, OperationId, LevelNumber) {

    object.prop('disabled', true);

    $('#HDFIsFromAddedOperation').val(IsFromAddedOperation);
    $('#HDFFBID').val(FBId);

    Year = $('#HDFYear').val();
    NoeFB = parseInt($('#HDFNoeFB').val());

    var $row = object.parent().parent();

    // حذف خطاهای قبلی
    $row.find('input[type=text]').each(function () {
        $(this).removeClass('ErrorValueStyle blinking');
    });

    var Sharh,
        Tedad,
        Tool,
        Arz,
        Ertefa,
        Vazn,
        Des,
        Check = true;

    firstObjectHasFocus = null;

    $row.find('input[type=text]').each(function () {

        var $input = $(this);
        var id = $input.attr('id');
        var value = $input.val();

        // -------------------------
        // شرح
        // -------------------------
        if (id.startsWith('txtSharh')) {

            Sharh = value;

        }

        // -------------------------
        // تابع بررسی Min / Max
        // -------------------------
        function CheckMinMax($input, fieldName) {

            var value = $input.val();

            // اگر خالی است
            if (value === undefined || value === null || value.trim() === '') {

                // اگر Essential نیست، مشکلی ندارد
                if (!$input.hasClass('Essential'))
                    return true;

                return false;
            }

            // حذف کاما
            value = value.replace(/\,/g, '');

            // بررسی عدد بودن
            if (!$.isNumeric(parseFloat(value)))
                return false;

            var numberValue = parseFloat(value);

            var min = $input.attr('data-min');
            var max = $input.attr('data-max');

            // -------------------------
            // بررسی Min
            // -------------------------
            if (min !== undefined &&
                min !== null &&
                min !== '' &&
                $.isNumeric(parseFloat(min))) {

                if (numberValue < parseFloat(min)) {

                    $input.addClass('ErrorValueStyle');

                    if (firstObjectHasFocus == null)
                        firstObjectHasFocus = $input;

                    toastr.warning(
                        `مقدار ${fieldName} نباید کمتر از ${min} باشد`,
                        'هشدار'
                    );

                    return false;
                }
            }

            // -------------------------
            // بررسی Max
            // -------------------------
            if (max !== undefined &&
                max !== null &&
                max !== '' &&
                $.isNumeric(parseFloat(max))) {

                if (numberValue > parseFloat(max)) {

                    $input.addClass('ErrorValueStyle');

                    if (firstObjectHasFocus == null)
                        firstObjectHasFocus = $input;

                    toastr.warning(
                        `مقدار ${fieldName} نباید بیشتر از ${max} باشد`,
                        'هشدار'
                    );

                    return false;
                }
            }

            return true;
        }


        // -------------------------
        // تعداد
        // -------------------------
        if (id.startsWith('txtTedad')) {

            var valueTedad = value ? value.replace(/\,/g, '') : '';

            if ($input.hasClass('Essential')) {

                if ($.isNumeric(parseFloat(valueTedad))) {

                    if (CheckMinMax($input, 'تعداد')) {

                        Tedad = valueTedad;
                        $input.removeClass('blinking');
                    }
                    else {
                        $input.addClass('ErrorValueStyle');
                        Check = false;
                    }

                }
                else {

                    $input.addClass('blinking');
                    $input.removeClass('TextEdit');

                    if (firstObjectHasFocus == null)
                        firstObjectHasFocus = $input;

                    Check = false;
                }

            }
            else {

                if ($.isNumeric(parseFloat(valueTedad))) {

                    if (CheckMinMax($input, 'تعداد'))
                        Tedad = valueTedad;
                    else
                        Check = false;

                }
                else {

                    Tedad = undefined;

                }
            }
        }

        // -------------------------
        // طول
        // -------------------------
        else if (id.startsWith('txtTool')) {

            var valueTool = value ? value.replace(/\,/g, '') : '';

            if ($input.hasClass('Essential')) {

                if ($.isNumeric(parseFloat(valueTool))) {

                    if (CheckMinMax($input, 'طول')) {

                        Tool = valueTool;
                        $input.removeClass('ErrorValueStyle');

                    }
                    else {

                        Check = false;

                    }

                }
                else {

                    $input.addClass('ErrorValueStyle');
                    $input.removeClass('TextEdit');

                    if (firstObjectHasFocus == null)
                        firstObjectHasFocus = $input;

                    Check = false;
                }

            }
            else {

                if ($.isNumeric(parseFloat(valueTool))) {

                    if (CheckMinMax($input, 'طول'))
                        Tool = valueTool;
                    else
                        Check = false;

                }
                else {

                    Tool = undefined;

                }
            }
        }

        // -------------------------
        // عرض
        // -------------------------
        else if (id.startsWith('txtArz')) {

            var valueArz = value ? value.replace(/\,/g, '') : '';

            if ($input.hasClass('Essential')) {

                if ($.isNumeric(parseFloat(valueArz))) {

                    if (CheckMinMax($input, 'عرض')) {

                        Arz = valueArz;
                        $input.removeClass('ErrorValueStyle');

                    }
                    else {

                        Check = false;

                    }

                }
                else {

                    $input.addClass('ErrorValueStyle');
                    $input.removeClass('TextEdit');

                    if (firstObjectHasFocus == null)
                        firstObjectHasFocus = $input;

                    Check = false;
                }

            }
            else {

                if ($.isNumeric(parseFloat(valueArz))) {

                    if (CheckMinMax($input, 'عرض'))
                        Arz = valueArz;
                    else
                        Check = false;

                }
                else {

                    Arz = undefined;

                }
            }
        }

        // -------------------------
        // ارتفاع
        // -------------------------
        else if (id.startsWith('txtErtefa')) {

            var valueErtefa = value ? value.replace(/\,/g, '') : '';

            if ($input.hasClass('Essential')) {

                if ($.isNumeric(parseFloat(valueErtefa))) {

                    if (CheckMinMax($input, 'ارتفاع')) {

                        Ertefa = valueErtefa;
                        $input.removeClass('ErrorValueStyle');

                    }
                    else {

                        Check = false;

                    }

                }
                else {

                    $input.addClass('ErrorValueStyle');
                    $input.removeClass('TextEdit');

                    if (firstObjectHasFocus == null)
                        firstObjectHasFocus = $input;

                    Check = false;
                }

            }
            else {

                if ($.isNumeric(parseFloat(valueErtefa))) {

                    if (CheckMinMax($input, 'ارتفاع'))
                        Ertefa = valueErtefa;
                    else
                        Check = false;

                }
                else {

                    Ertefa = undefined;

                }
            }
        }

        // -------------------------
        // وزن
        // -------------------------
        else if (id.startsWith('txtVazn')) {

            var valueVazn = value ? value.replace(/\,/g, '') : '';

            if ($input.hasClass('Essential')) {

                if ($.isNumeric(parseFloat(valueVazn))) {

                    if (CheckMinMax($input, 'وزن')) {

                        Vazn = valueVazn;
                        $input.removeClass('ErrorValueStyle');

                    }
                    else {

                        Check = false;

                    }

                }
                else {

                    $input.addClass('ErrorValueStyle');
                    $input.removeClass('TextEdit');

                    if (firstObjectHasFocus == null)
                        firstObjectHasFocus = $input;

                    Check = false;
                }

            }
            else {

                if ($.isNumeric(parseFloat(valueVazn))) {

                    if (CheckMinMax($input, 'وزن'))
                        Vazn = valueVazn;
                    else
                        Check = false;

                }
                else {

                    Vazn = undefined;

                }
            }
        }

        // -------------------------
        // توضیحات
        // -------------------------
        else if (id.startsWith('txtDes')) {

            Des = value;

        }

    });


    // Focus روی اولین فیلد خطادار
    if (firstObjectHasFocus != null)
        firstObjectHasFocus.focus();


    ForItem = '';

    if (IsFromAddedOperation == 'true')
        ForItem = $('#HDFItemsFBShomareh').val();


    // -------------------------
    // اگر خطایی وجود نداشت
    // -------------------------
    if (Check) {

        BarAvordUserId = $('#HDFBarAvordUserID').val();

        var vardata11 = new Object();

        vardata11.Sharh = Sharh;
        vardata11.Tedad = Tedad === undefined ? null : Tedad;
        vardata11.Tool = Tool === undefined ? null : Tool;
        vardata11.Arz = Arz === undefined ? null : Arz;
        vardata11.Ertefa = Ertefa === undefined ? null : Ertefa;
        vardata11.Vazn = Vazn === undefined ? null : Vazn;
        vardata11.Des = Des;

        vardata11.FBId = FBId;
        vardata11.BarAvordUserId = BarAvordUserId;
        vardata11.OperationId = OperationId;
        vardata11.ForItem = ForItem;
        vardata11.Year = Year;
        vardata11.NoeFB = NoeFB;
        vardata11.LevelNumber = LevelNumber;


        $.ajax({

            type: "POST",

            url: '/RizMetreUser/ConfirmRizMetreUsers',

            dataType: "json",

            data: JSON.stringify(vardata11),

            contentType: "application/json; charset=utf-8",

            success: function (data) {

                debugger;

                object.removeAttr('disabled');

                //$loading.remove();

                var info = data.split('_');

                if (info[0] == "OK") {

                    ClearInput(object);

                    GetRizMetreWithFBId(LevelNumber);

                    $("#divItemsAddedAndRel" + NoeFB + "_" + info[1] + " div[id^='divShowRizMetre']").each(function () {

                        if ($(this).css("display") === "block") {

                            $(this).slideUp();

                        }

                    });

                    toastr.success(
                        'ریزه متره جدید بدرستی درج گردید',
                        'موفقیت'
                    );

                }
                else {

                    toastr.info(
                        'مشکل در درج ریزه متره جدید',
                        'اطلاع'
                    );
                }

            },

            error: function (msg) {

                toastr.error(
                    'مشکل در درج ریزه متره جدید',
                    'خطا'
                );

            }

        });

    }
    else {

        object.removeAttr('disabled');

        //$loading.remove();

        // فقط اگر خطای Min/Max قبلاً پیام نداده باشد،
        // پیام کلی نمایش داده شود
        if (firstObjectHasFocus == null) {

            toastr.warning(
                'مقادیر مشخص شده را وارد نمایید',
                'هشدار'
            );

        }

    }
}










// function SaveRMUClick(object, FBId, IsFromAddedOperation, OperationId, LevelNumber) {
//     object.prop('disabled', true);
//     $('#HDFIsFromAddedOperation').val(IsFromAddedOperation);
//     $('#HDFFBID').val(FBId);
//     Year = $('#HDFYear').val();
//     NoeFB = parseInt($('#HDFNoeFB').val());
//     object.parent().parent().find('input[type=text]').each(function () {
//         $(this).removeClass('ErrorValueStyle');
//     });
//     var Sharh, Tedad, Tool, Arz, Ertefa, Vazn, Des, Check = true;
//     firstObjectHasFocus = null;
//     object.parent().parent().find('input[type=text]').each(function () {
//         if ($(this).attr('id').startsWith('txtSharh'))
//             Sharh = $(this).val();
//         if ($(this).attr('id').startsWith('txtTedad')) {
//             if ($(this).hasClass('Essential')) {
//                 if ($.isNumeric(parseFloat($(this).val()))) {
//                     Tedad = $(this).val().replace(/\,/g, '');
//                     $(this).removeClass('blinking');
//                 }
//                 else {
//                     $(this).addClass('blinking');
//                     $(this).removeClass('TextEdit');
//                     Check = false;
//                 }
//             }
//             else {
//                 if ($.isNumeric(parseFloat($(this).val()))) {
//                     Tedad = $(this).val().replace(/\,/g, '');
//                     $(this).removeClass('blinking');
//                 }
//                 else
//                     Tedad = undefined;
//             }
//         }
//         else if ($(this).attr('id').startsWith('txtTool')) {
//             if ($(this).hasClass('Essential')) {
//                 if ($.isNumeric(parseFloat($(this).val()))) {
//                     Tool = $(this).val().replace(/\,/g, '');
//                     $(this).removeClass('ErrorValueStyle');
//                 }
//                 else {
//                     $(this).addClass('ErrorValueStyle');
//                     $(this).removeClass('TextEdit');
//                     if (firstObjectHasFocus == null) {
//                         firstObjectHasFocus = $(this);
//                     }
//                     Check = false;
//                 }
//             }
//             else {
//                 if ($.isNumeric(parseFloat($(this).val()))) {
//                     Tool = $(this).val().replace(/\,/g, '');
//                     $(this).removeClass('ErrorValueStyle');
//                 }
//                 else
//                     Tool = undefined;
//             }
//         }
//         else if ($(this).attr('id').startsWith('txtArz')) {
//             if ($(this).hasClass('Essential')) {
//                 if ($.isNumeric(parseFloat($(this).val()))) {
//                     Arz = $(this).val().replace(/\,/g, '');
//                     $(this).removeClass('ErrorValueStyle');
//                 }
//                 else {
//                     $(this).addClass('ErrorValueStyle');
//                     $(this).removeClass('TextEdit');
//                     if (firstObjectHasFocus == null) {
//                         firstObjectHasFocus = $(this);
//                     }
//                     Check = false;
//                 }
//             }
//             else {
//                 if ($.isNumeric(parseFloat($(this).val()))) {
//                     Arz = $(this).val().replace(/\,/g, '');
//                     $(this).removeClass('ErrorValueStyle');
//                 }
//                 else
//                     Arz = undefined;
//             }
//         }
//         else if ($(this).attr('id').startsWith('txtErtefa')) {
//             if ($(this).hasClass('Essential')) {
//                 if ($.isNumeric(parseFloat($(this).val()))) {
//                     Ertefa = $(this).val().replace(/\,/g, '');
//                     $(this).removeClass('ErrorValueStyle');
//                 }
//                 else {
//                     $(this).addClass('ErrorValueStyle');
//                     $(this).removeClass('TextEdit');
//                     if (firstObjectHasFocus == null) {
//                         firstObjectHasFocus = $(this);
//                     }
//                     Check = false;
//                 }
//             }
//             else {
//                 if ($.isNumeric(parseFloat($(this).val()))) {
//                     Ertefa = $(this).val().replace(/\,/g, '');
//                     $(this).removeClass('ErrorValueStyle');
//                 }
//                 else
//                     Ertefa = undefined;
//             }
//         }
//         else if ($(this).attr('id').startsWith('txtVazn')) {
//             if ($(this).hasClass('Essential')) {
//                 if ($.isNumeric(parseFloat($(this).val()))) {
//                     Vazn = $(this).val().replace(/\,/g, '');
//                     $(this).removeClass('ErrorValueStyle');
//                 }
//                 else {
//                     $(this).addClass('ErrorValueStyle');
//                     $(this).removeClass('TextEdit');
//                     if (firstObjectHasFocus == null) {
//                         firstObjectHasFocus = $(this);
//                     }
//                     Check = false;
//                 }
//             }
//             else {
//                 if ($.isNumeric(parseFloat($(this).val()))) {
//                     Vazn = $(this).val().replace(/\,/g, '');
//                     $(this).removeClass('ErrorValueStyle');
//                 }
//                 else
//                     Vazn = undefined;
//             }
//         }
//         else if ($(this).attr('id').startsWith('txtDes'))
//             Des = $(this).val();

//     });


//     if (firstObjectHasFocus != null)
//         firstObjectHasFocus.focus();

//     ForItem = '';
//     if (IsFromAddedOperation == 'true')
//         ForItem = $('#HDFItemsFBShomareh').val();


//     if (Check) {
//         BarAvordUserId = $('#HDFBarAvordUserID').val();

//         var vardata11 = new Object();
//         vardata11.Sharh = Sharh;
//         vardata11.Tedad = Tedad === undefined ? null : Tedad;
//         vardata11.Tool = Tool === undefined ? null : Tool;
//         vardata11.Arz = Arz === undefined ? null : Arz;
//         vardata11.Ertefa = Ertefa === undefined ? null : Ertefa;
//         vardata11.Vazn = Vazn === undefined ? null : Vazn;
//         vardata11.Des = Des;
//         vardata11.FBId = FBId;
//         vardata11.BarAvordUserId = BarAvordUserId;
//         vardata11.OperationId = OperationId;
//         vardata11.ForItem = ForItem;
//         vardata11.Year = Year;
//         vardata11.NoeFB = NoeFB;
//         vardata11.LevelNumber = LevelNumber;

//         $.ajax({
//             type: "POST",
//             url: '/RizMetreUser/ConfirmRizMetreUsers',
//             dataType: "json",
//             data: JSON.stringify(vardata11),
//             contentType: "application/json; charset=utf-8",
//             success: function (data) {


//                 debugger;

//                 object.removeAttr('disabled');
//                 $loading.remove();

//                 var info = data.split('_');
//                 if (info[0] == "OK") {

//                     ClearInput(object);
//                     GetRizMetreWithFBId(LevelNumber);
//                     $("#divItemsAddedAndRel" + NoeFB + "_" + info[1] + " div[id^='divShowRizMetre']").each(function () {
//                         if ($(this).css("display") === "block") {

//                             $(this).slideUp();

//                             //thisId= this.id.substring(15, this.id.length);
//                             //// پیدا کردن نزدیک‌ترین span با کلاس spanStyleMitraSmall
//                             //var $span = $("#CK" + thisId).find("span.spanLakeGiriStyle").first();
//                             //if ($span.length === 0) {
//                             //    $span = $(this).closest("div").find("span .spanLakeGiriStyle").first();
//                             //}
//                             //if ($span.length > 0) {
//                             //    $span.click();
//                             //}
//                         }
//                     });

//                     //CheckOperationHasExistActiveCondition(OperationId);
//                     toastr.success('ریزه متره جدید بدرستی درج گردید', 'موفقیت');
//                 }
//                 else
//                     toastr.info('مشکل در درج ریزه متره جدید', 'اطلاع');
//             },
//             error: function (msg) {
//                 toastr.error('مشکل در درج ریزه متره جدید', 'خطا');
//             }
//         });
//     }
//     else {
//         object.removeAttr('disabled');
//         $loading.remove();

//         toastr.warning('مقادیر مشخص شده را وارد نمایید', 'هشدار');
//     }
// }

function ClearInput(object) {
    object.parent().parent().find('input[type=text]').each(function () {
        if (this.id.startsWith("txtSharh")) {
            $(this).focus();
        }
        $(this).val('');
    });
}

function GetRizMetreUsers(LevelNumber) {
    BarAvordUserId = $('#HDFBarAvordUserID').val();
    IsFromAddedOperation = $('#HDFIsFromAddedOperation').val();

    if (IsFromAddedOperation == 'true')
        OperationId = $('#HDFOperationIdN').val();
    else
        OperationId = $('#HDFOperationId').val();
    FBId = $('#HDFFBID').val();
    NoeFB = parseInt($('#HDFNoeFB').val());
    Year = $('#HDFYear').val();
    ForItem = '';
    if (IsFromAddedOperation == 'true')
        ForItem = $('#HDFItemsFBShomareh').val();

    var vardata = new Object();
    vardata.FBId = FBId;
    vardata.IsFromAddedOperation = IsFromAddedOperation;
    vardata.BarAvordUserId = BarAvordUserId;
    vardata.NoeFB = NoeFB;
    vardata.Year = Year;
    vardata.OperationId = OperationId;
    vardata.ForItem = ForItem;
    vardata.LevelNumber = LevelNumber;

    $.ajax({
        type: "POST",
        url: '/RizMetreUser/GetRizMetreUsers',
        dataType: "json",
        //data: '{FBId:' + FBId + ',IsFromAddedOperation:' + "'" + IsFromAddedOperation
        //    + "'" + ',BarAvordUserId:' + BarAvordUserId 
        //    + ',NoeFB:' + "'eec10c4b-5452-4677-a22b-ab5ea9b4e3f0'" + ',Year:' + "'1397'" + '}',
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        success: function (data) {

            var info = data.split('_');
            if (info[0] == "OK") {
                if (LevelNumber === 1) {
                    if (IsFromAddedOperation == 'false') {
                        $('#uldiva' + OperationId).html(info[1]);
                        //$('#divShowRizMetre').html($('#uldiva' + OperationId).html());
                    }
                    else
                        $('#nuldivna' + OperationId).html(info[1]);

                    CurrentTabOpen = $('#HDFCurrentTabOpen').val();
                    if (CurrentTabOpen == 'divAddedItems') {
                        $('#divAddedItems' + OperationId).css('display', 'block');
                        $('#btnRelItems' + OperationId).css('background-color', 'rgb(129, 173, 201)');
                        $('#btnAddedItems' + OperationId).css('background-color', 'rgb(46, 112, 155)');
                    }
                    else {
                        $('#divRelItems' + OperationId).css('display', 'block');
                        $('#btnAddedItems' + OperationId).css('background-color', 'rgb(129, 173, 201)');
                        $('#btnRelItems' + OperationId).css('background-color', 'rgb(46, 112, 155)');
                    }
                }
                else if (LevelNumber === 2) {
                    if (IsFromAddedOperation == 'false') {
                        $('#PopupViewNextRutinTreeView').find('#uldiva' + OperationId).html(info[1]);
                        //$('#PopupViewNextRutinTreeView').find('#divShowRizMetre').html($('#uldiva' + OperationId).html());
                    }
                    else
                        $('#PopupViewNextRutinTreeView').find('#nuldivna' + OperationId).html(info[1]);

                    CurrentTabOpen = $('#HDFCurrentTabOpen').val();
                    if (CurrentTabOpen == 'divAddedItems') {
                        $('#PopupViewNextRutinTreeView').find('#divAddedItems' + OperationId).css('display', 'block');
                        $('#PopupViewNextRutinTreeView').find('#btnRelItems' + OperationId).css('background-color', 'rgb(129, 173, 201)');
                        $('#PopupViewNextRutinTreeView').find('#btnAddedItems' + OperationId).css('background-color', 'rgb(46, 112, 155)');
                    }
                    else {
                        $('#PopupViewNextRutinTreeView').find('#divRelItems' + OperationId).css('display', 'block');
                        $('#PopupViewNextRutinTreeView').find('#btnAddedItems' + OperationId).css('background-color', 'rgb(129, 173, 201)');
                        $('#PopupViewNextRutinTreeView').find('#btnRelItems' + OperationId).css('background-color', 'rgb(46, 112, 155)');
                    }
                }
                else if (LevelNumber === 0) {
                    if (IsFromAddedOperation == 'false') {
                        $('#PopupViewNextRutin').find('#uldiva' + OperationId).html(info[1]);
                    }
                    else
                        $('#PopupViewNextRutin').find('#nuldivna' + OperationId).html(info[1]);
                }
            }
            else
                toastr.info('مشکل در بارگزاری', 'اطلاع');
        },
        error: function (msg) {
            toastr.error('مشکل در بارگزاری', 'خطا');
        }
    });
}

function CheckOperationHasAddedOperationsOnly(object, AddedOperationId, ItemsFBShomareh, Type, LatinName, LevelNumber) {
    object.parent().find('input').prop("checked", true);
    $('#HDFOperationHasAddedOperations').val(Type);
    $('#HDFItemsFBShomareh').val(ItemsFBShomareh);
    Year = $('#HDFYear').val();
    BarAvordUserID = $('#HDFBarAvordUserID').val();
    NoeFB = parseInt($('#HDFNoeFB').val());
    var vardata = new Object();
    vardata.AddedOperationId = AddedOperationId;
    vardata.ItemsFBShomareh = ItemsFBShomareh;
    vardata.BarAvordUserId = BarAvordUserID;
    vardata.Type = Type;
    vardata.Year = Year;
    vardata.NoeFB = NoeFB;
    vardata.LevelNumber = LevelNumber;

    $.ajax({
        type: "POST",
        url: '/Operation_ItemsFB/CheckRutin',
        dataType: "json",
        //data: '{AddedOperationId:' + AddedOperationId
        //    + ',ItemsFBShomareh:' + "'" + ItemsFBShomareh + "'"
        //    + ',BarAvordId:' + BarAvordID + ',Type:' + Type + ',Year:'+Year+',NoeFB:'+"'"+NoeFB+"'"+'}',
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        success: function (data) {

            if (Type == 5) {
                //arrNotIn = [];
                //a = [];
                //for (var i = 0; i < data.length; i++) {
                //    ID = data[i].id;
                //    ParentId = data[i].parentId;
                //    if (ParentId == 1404003)
                //        ParentId = '#';
                //    OperationName = data[i].operationName;
                //    FunctionCall = data[i].functionCall;
                //    Sharh = data[i].sharh;
                //    ItemsFBShomareh = data[i].itemsFBShomareh;
                //    if (ItemsFBShomareh != '') {
                //        text = "<a id=\"a" + ID + "\" onclick=\"OperationClickFromSecondLevel('" + ID + "')\" >" + ItemsFBShomareh
                //            + " - " + Sharh + "<span id=\"span" + ID + "\"></span> </a><span id=\"spanOpShomareh" + ItemsFBShomareh + "\"></span></li>";
                //        text += "<div id=\"ula" + ID + "\" class=\"row\" style=\"display: none; margin:10px\">";
                //        text += "<div id=\"uldiva" + ID + "\" class=\"col-md-12\" style=\"border:1px solid #79c7ea;padding-left:0px;padding-right:0px;text-align:center;border-radius:5px !important;\">";
                //        text += "</div></div>";

                //    }
                //    else {
                //        if (FunctionCall != '') {
                //            text = "<a onclick=\"" + FunctionCall + "('" + ID + "')\" id=\"a" + ID + "\">" + OperationName + "<span id=\"span" + ID + "\"></span> "
                //                + "</a><span id=\"spanOpShomareh" + ID + "\"></span><div style=\"margin-top:5px\" id=\"ula" + ID + "\"></div>";
                //        }
                //        else {
                //            text = "<a id=\"a" + ID + "\">" + OperationName + "<span id=\"span" + ID + "\"></span> "
                //                + "</a><span id=\"spanOpShomareh" + ID + "\"></span><div id=\"ula" + ID + "\"></div>";
                //        }
                //    }
                //    if (ParentId != -1) {
                //        if ($.inArray(ParentId, arrNotIn) == -1)
                //            a.push({ 'id': ID, 'parent': ParentId, 'text': text });
                //        else
                //            arrNotIn.push(ID);
                //    }
                //    else {
                //        arrNotIn.push(ID);
                //    }
                //}


                //TreeView1(a, plugin);

                const container = $('#PopupViewNextRutinTreeView').find('#jsTree1');
                container.html('');
                // حلقه برای پیدا کردن والدها (parentid == '#')
                data.forEach(parent => {
                    if (parent.parentId === 1404003) {
                        const radioHtml = `
                                <div class="parent">
                                <h4 style="background-color: #2e043c;color: #fff;padding: 2px 15px;"> ${parent.operationName}</h4>
                                <div class="children" id="children-${parent.id}"></div>
                                </div>
                        `;
                        container.append(radioHtml);
                    }
                });

                // حلقه برای پیدا کردن بچه‌ها
                data.forEach(child => {
                    if (child.parentId !== 1404003) {
                        const childHtml = `
                        <div><a id="a${child.id}" class="child-item" onclick="OperationClickSecondLevel('${child.id}')">-${child.itemsFBShomareh} - ${child.sharh}</a></div>
                        <div style="display:none" id="ula${child.id}">
                        <div id="uldiva${child.id}"></div>
                        </div>
                        `;
                        $(`#children-${child.parentId}`).append(childHtml);
                    }
                });

                $('#aViewNextRutinTreeView').click();

            }


            var info = data.split('_');
            if (info[0] == "OK") {

                $('#aViewNextRutin').click();
                $('#PopupViewNextRutin').find('#divNextRutin').html(info[1]);
                $('li a').each(function () {

                    $(this).addClass('OpMenuStyle');
                    $(this).click(function () {
                        $('#PopupViewNextRutin').find('#nul' + $(this).attr('id')).toggle(1000);
                    });
                });

            }
            else if (info[0] == "NOK") {
                toastr.info(info[1], 'اطلاع');
            }
        },
        error: function (msg) {
            toastr.error('مشکل در بارگزاری ریزه متره انتخابی', 'خطا');
        }
    });
}

function CheckOperationHasAddedOperations(object, AddedOperationId, ItemsFBShomareh, Type, LatinName, LevelNumber) {
    //if (LatinName == 'LakeGiri') {
    //    $('#spanFieldShomarehName').html('لکه گیری');
    //    $('#Grid' + ItemsFBShomareh).find("input[id^='CKLakeGiri']").each(function () {
    //        $('#' + $(this).attr('id')).removeClass('displayNone');
    //    });
    //}
    //else {
    if (object.is(':checked')) {
        $('#HDFOperationHasAddedOperations').val(Type);
        $('#HDFItemsFBShomareh').val(ItemsFBShomareh);
        BarAvordUserID = $('#HDFBarAvordUserID').val();
        Year = $('#HDFYear').val();
        BarAvordUserID = $('#HDFBarAvordUserID').val();
        NoeFB = parseInt($('#HDFNoeFB').val());
        var vardata = new Object();
        vardata.AddedOperationId = AddedOperationId;
        vardata.ItemsFBShomareh = ItemsFBShomareh;
        vardata.BarAvordUserId = BarAvordUserID;
        vardata.Type = Type;
        vardata.Year = Year;
        vardata.NoeFB = NoeFB;
        vardata.LevelNumber = LevelNumber;
        $.ajax({
            type: "POST",
            url: '/Operation_ItemsFB/CheckRutin',
            dataType: "json",
            //data: '{AddedOperationId:' + AddedOperationId
            //    + ',ItemsFBShomareh:' + "'" + ItemsFBShomareh + "'"
            //    + ',BarAvordId:' + BarAvordID + ',Type:' + Type + ',Year:' + Year + ',NoeFB:' + "'" + NoeFB + "'" + '}',
            data: JSON.stringify(vardata),
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                var info = data.split('_');
                if (info[0] == "OK") {
                    $('#aViewNextRutin').click();
                    $('#PopupViewNextRutin').find('#divNextRutin').html(info[1]);
                    $('li a').each(function () {
                        $(this).addClass('OpMenuStyle');
                        $(this).click(function () {
                            $('#PopupViewNextRutin').find('#nul' + $(this).attr('id')).toggle(1000);
                        });
                    });
                }
                else if (info[0] == "NOK") {
                    toastr.info(info[1], 'اطلاع');
                }
            },
            error: function (msg) {
                toastr.error('مشکل در بارگزاری ریزه متره انتخابی', 'خطا');
            }
        });
    }
    else {
        var vardata = new Object();
        vardata.BarAvordId = BarAvordUserId;
        vardata.ItemsFBShomareh = ItemsFBShomareh;
        vardata.LevelNumber = LevelNumber;
        vardata.Type = Type;
        $.ajax({
            type: "POST",
            url: '/RizMetreUser/DeleteItemsFBShomarehValueShomareh',
            dataType: "json",
            data: JSON.stringify(vardata),
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                var info = data.split('_');
                if (info[0] == "OK") {
                    toastr.success('حذف بدرستی صورت گرفت', 'موفقیت');
                }
                else if (info[0] == "NOK") {
                    toastr.error('مشکل در حذف', 'خطا');
                }
            },
            error: function (msg) {
                toastr.error('مشکل در حذف', 'خطا');
            }
        });
    }
}

function SumAllForParent(object) {
    Meghdar = 0;
    object.find('li a').each(function () {
        if ($(this).parent().find('ul').length != 0) {
            Meghdar += SumAllForParent($(this).parent().find('ul'));
            Id = ($(this).attr('id').substring(1, $(this).attr('id').length));
            if ($(this).parent().find('#spanOpShomareh' + Id).html() == '')
                $(this).parent().find('#spanOpShomareh' + Id).html(Meghdar);
        }
        else {
            {
                Meghdar += parseFloat($(this).parent().find('span').html().replace(/\,/g, ''));
            }
        }
    });
    return Meghdar;
    //else
    //    alert($(this).attr('id'));
}

////////////
function RizMetreSelectClick(Id) {
    $('#HDFRizMetreId').val(Id);
}

////////////
function UpdateRMUClick(Id) {
    Obj = $('#iUpdate' + Id);
    Obj.prop('disabled', true);
    $('#HDFIsFromAddedOperation').val('false');
    FBId = $('#HDFFBID').val();
    OperationId = $('#HDFOperationId').val();

    NoeFB = parseInt($('#HDFNoeFB').val());
    Year = $('#HDFYear').val();
    var Sharh, Tedad, Tool, Arz, Ertefa, Vazn, Des, Check = true;
    Sharh = $('#txtSharh' + Id).val();
    Des = $('#txtDes' + Id).val();

    //if ($('#txtTedad' + Id).hasClass('HasEnteringValue')) {
    //    if ($.isNumeric(parseFloat($('#txtTedad' + Id).val()))) {
    //        Tedad = $('#txtTedad' + Id).val().replace(/\,/g, '');
    //    }
    //    else {
    //        $('#txtTedad' + Id).addClass('ErrorValueStyle');
    //        Check = false;
    //    }
    //}
    //else Tedad = 0;

    if ($('#txtTedad' + Id).val() != '') {
        if ($.isNumeric(parseFloat($('#txtTedad' + Id).val()))) {
            Tedad = $('#txtTedad' + Id).val().replace(/\,/g, '');
        }
        else {
            $('#txtTedad' + Id).addClass('ErrorValueStyle');
            Check = false;
        }
    }

    firstObjectHasFocus = null;
    if ($('#txtTool' + Id).hasClass('HasEnteringValue')) {
        if ($.isNumeric(parseFloat($('#txtTool' + Id).val()))) {
            Tool = $('#txtTool' + Id).val().replace(/\,/g, '');
            $('#txtTool' + Id).removeClass('ErrorValueStyle');
        }
        else {
            $('#txtTool' + Id).addClass('ErrorValueStyle');
            $('#txtTool' + Id).removeClass('TextEdit');
            if (firstObjectHasFocus == null) {
                firstObjectHasFocus = $('#txtTool' + Id);
            }
            Check = false;
        }
    }
    else Tool = undefined;

    if ($('#txtArz' + Id).hasClass('HasEnteringValue')) {
        if ($.isNumeric(parseFloat($('#txtArz' + Id).val()))) {
            Arz = $('#txtArz' + Id).val().replace(/\,/g, '');
            $('#txtArz' + Id).removeClass('ErrorValueStyle');

        }
        else {
            $('#txtArz' + Id).addClass('ErrorValueStyle');
            $('#txtArz' + Id).removeClass('TextEdit');
            if (firstObjectHasFocus == null) {
                firstObjectHasFocus = $('#txtArz' + Id);
            }
            Check = false;
        }
    }
    else Arz = undefined;

    if ($('#txtErtefa' + Id).hasClass('HasEnteringValue')) {
        if ($.isNumeric(parseFloat($('#txtErtefa' + Id).val()))) {
            Ertefa = $('#txtErtefa' + Id).val().replace(/\,/g, '');
            $('#txtErtefa' + Id).removeClass('ErrorValueStyle');

        }
        else {
            $('#txtErtefa' + Id).addClass('ErrorValueStyle');
            $('#txtErtefa' + Id).removeClass('TextEdit');
            if (firstObjectHasFocus == null) {
                firstObjectHasFocus = $('#txtErtefa' + Id);
            }
            Check = false;
        }
    }
    else Ertefa = undefined;

    if ($('#txtVazn' + Id).hasClass('HasEnteringValue')) {
        if ($.isNumeric(parseFloat($('#txtVazn' + Id).val()))) {
            Vazn = $('#txtVazn' + Id).val().replace(/\,/g, '');
            $('#txtVazn' + Id).removeClass('ErrorValueStyle');

        }
        else {
            $('#txtVazn' + Id).addClass('ErrorValueStyle');
            $('#txtVazn' + Id).removeClass('TextEdit');
            if (firstObjectHasFocus == null) {
                firstObjectHasFocus = $('#txtVazn' + Id);
            }
            Check = false;
        }
    }
    else Vazn = undefined;

    if (firstObjectHasFocus != null)
        firstObjectHasFocus.focus();
    var vardata = new Object();
    vardata.Id = Id;
    vardata.Sharh = Sharh;
    vardata.Tedad = Tedad === undefined ? null : Tedad;
    vardata.Tool = Tool === undefined ? null : Tool;
    vardata.Arz = Arz === undefined ? null : Arz;
    vardata.Ertefa = Ertefa === undefined ? null : Ertefa;
    vardata.Vazn = Vazn === undefined ? null : Vazn;
    vardata.Des = Des;
    vardata.NoeFBId = NoeFB;
    vardata.Year = Year;
    vardata.FBId = FBId;
    vardata.OperationId = OperationId;
    vardata.LevelNumber = 1;
    if (Check) {
        $.ajax({
            type: "POST",
            url: '/RizMetreUser/UpdateRizMetreUsers',
            dataType: "json",
            data: JSON.stringify(vardata),
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                Obj.removeAttr('disabled');

                var info = data.split('_');
                if (info[0] == "OK") {
                    $("#divItemsAddedAndRel" + NoeFB + "_" + info[1] + " div[id^='divShowRizMetre']").each(function () {
                        if ($(this).css("display") === "block") {
                            $(this).slideUp();
                        }
                    });

                    GetRizMetreWithFBId(1);
                    toastr.success('ریزه متره انتخابی بدرستی ویرایش گردید', 'موفقیت');
                }
                else
                    toastr.info('مشکل در ویرایش ریزه متره انتخابی', 'اطلاع');
            },
            error: function (msg) {
                toastr.error('مشکل در ویرایش ریزه متره انتخابی', 'خطا');
            }
        });
    }
    else {
        Obj.removeAttr('disabled');
        toastr.warning('مقادیر مشخص شده را وارد نمایید', 'هشدار');
    }
}

////////////
function UpdateNRMUClick(Id, OperationId, FBId) {
    $('#HDFIsFromAddedOperation').val('true');
    NoeFB = parseInt($('#HDFNoeFB').val());
    Year = $('#HDFYear').val();
    var Sharh, Tedad, Tool, Arz, Ertefa, Vazn, Des, Check = true;
    Sharh = $('#txtSharh' + Id).val();
    Des = $('#txtDes' + Id).val();
    $('#HDFOperationIdN').val(OperationId);

    //if ($('#txtTedad' + Id).hasClass('HasEnteringValue')) {
    if ($.isNumeric(parseFloat($('#txtTedad' + Id).val()))) {
        Tedad = $('#txtTedad' + Id).val().replace(/\,/g, '');
    }
    else {
        $('#txtTedad' + Id).addClass('ErrorValueStyle');
        Check = false;
    }
    //}
    //else Tedad = 0;

    //if ($('#txtTool' + Id).hasClass('HasEnteringValue')) {
    if ($.isNumeric(parseFloat($('#txtTool' + Id).val()))) {
        Tool = $('#txtTool' + Id).val().replace(/\,/g, '');
    }
    else {
        $('#txtTool' + Id).addClass('ErrorValueStyle');
        Check = false;
    }
    //}
    //else Tool = 0;

    //if ($('#txtArz' + Id).hasClass('HasEnteringValue')) {
    if ($.isNumeric(parseFloat($('#txtArz' + Id).val()))) {
        Arz = $('#txtArz' + Id).val().replace(/\,/g, '');
    }
    else {
        $('#txtArz' + Id).addClass('ErrorValueStyle');
        Check = false;
    }
    //}
    //else Arz = 0;

    //if ($('#txtErtefa' + Id).hasClass('HasEnteringValue')) {
    if ($.isNumeric(parseFloat($('#txtErtefa' + Id).val()))) {
        Ertefa = $('#txtErtefa' + Id).val().replace(/\,/g, '');
    }
    else {
        $('#txtErtefa' + Id).addClass('ErrorValueStyle');
        Check = false;
    }
    //}
    //else Ertefa = 0;

    //if ($('#txtVazn' + Id).hasClass('HasEnteringValue')) {
    if ($.isNumeric(parseFloat($('#txtVazn' + Id).val()))) {
        Vazn = $('#txtVazn' + Id).val().replace(/\,/g, '');
    }
    else {
        $('#txtVazn' + Id).addClass('ErrorValueStyle');
        Check = false;
    }
    //}
    //else Vazn = 0;



    var vardata = new Object();
    vardata.Id = Id;
    vardata.Sharh = Sharh;
    vardata.Tedad = Tedad;
    vardata.Tool = Tool;
    vardata.Arz = Arz;
    vardata.Ertefa = Ertefa;
    vardata.Vazn = Vazn;
    vardata.Des = Des;
    vardata.NoeFB = NoeFB;
    vardata.Year = Year;
    vardata.LevelNumber = 0;


    if (Check) {
        $.ajax({
            type: "POST",
            url: 'RizMetreUser/UpdateRizMetreN',
            dataType: "json",
            //data: '{Id:' + Id + ',Sharh:' + "'" + Sharh + "'"
            //    + ',Tedad:' + Tedad + ',Tool:' + Tool + ',Arz:' + Arz
            //    + ',Ertefa:' + Ertefa + ',Vazn:' + Vazn + ',Des:' + "'" + Des + "'"
            //    + ',NoeFB:' + "'" + NoeFB + "'" + ',Year:' + Year + '}',
            data: JSON.stringify(vardata),
            contentType: "application/json; charset=utf-8",
            success: function (data) {


                var info = data.split('_');
                if (info[0] == "OK") {
                    GetRizMetreN(FBId);
                    toastr.success('ریزه متره انتخابی بدرستی ویرایش گردید', 'موفقیت');
                }
                else
                    toastr.info('مشکل در ویرایش ریزه متره انتخابی', 'اطلاع');
            },
            error: function (msg) {
                toastr.error('مشکل در ویرایش ریزه متره انتخابی', 'خطا');
            }
        });
    }
    else
        toastr.warning('مقادیر مشخص شده را وارد نمایید', 'هشدار');
}
/////
function GetRizMetreN(FBId) {

    BarAvordUserId = $('#HDFBarAvordUserID').val();
    IsFromAddedOperation = $('#HDFIsFromAddedOperation').val();

    if (IsFromAddedOperation == 'true')
        OperationId = $('#HDFOperationIdN').val();
    else
        OperationId = $('#HDFOperationId').val();
    NoeFB = parseInt($('#HDFNoeFB').val());
    Year = $('#HDFYear').val();
    ForItem = '';
    //if (IsFromAddedOperation == 'true')
    ForItem = $('#HDFItemsFBShomareh').val();

    var vardata = new Object();
    vardata.FBId = FBId;
    vardata.IsFromAddedOperation = IsFromAddedOperation;
    vardata.BarAvordUserId = BarAvordUserId;
    vardata.NoeFB = NoeFB;
    vardata.Year = Year;
    vardata.OperationId = OperationId;
    vardata.ForItem = ForItem;
    vardata.LevelNumber = 1;

    $.ajax({
        type: "POST",
        url: 'RizMetreUser/GetRizMetreNRMU',
        dataType: "json",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        success: function (DtRizMetreUsers) {


            var strOp = "";

            // Header
            strOp += '<div class="row styleHeaderTable">';
            strOp += '<div class="col-md-1 spanStyleMitraSmall">شماره</div>';
            strOp += '<div class="col-md-2 spanStyleMitraSmall">شرح</div>';

            var headers = ['تعداد', 'طول', 'عرض', 'ارتفاع', 'وزن یا...', 'مقدار جزء'];
            for (var i = 0; i < headers.length; i++) {
                strOp += '<div class="col-md-1 spanStyleMitraSmall">';
                strOp += '<div style="padding-bottom:3px;border-bottom:1px solid #fff">' + headers[i] + '</div>';
                //strOp += '<div class="VahedStyle">' + $.trim(DtItemsFields[i].Vahed) + '</div>';
                strOp += '</div>';
            }

            strOp += '<div class="col-md-2 spanStyleMitraSmall">توضیحات</div>';
            strOp += '<div class="col-md-1 spanStyleMitraSmall"><span>ویرایش/حذف</span></div>';
            strOp += '</div>';

            // Rows
            $.each(DtRizMetreUsers, function (index, row) {

                var id = row.id;

                strOp += '<div class="row styleRowTable" onclick="RizMetreSelectClick(\'' + id + '\')">';
                strOp += '<div class="col-md-1"><span class="RMRowStyle">' + row.shomareh + '</span></div>';
                strOp += '<div class="col-md-2"><input type="text" class="form-control TextEdit spanStyleMitraSmall" id="txtSharh' + id + '" value="' + row.sharh + '"/></div>';
                strOp += '<div class="col-md-1"><input type="text" class="form-control TextEdit spanStyleMitraSmall" id="txtTedad' + id + '" value="' + row.tedad + '"/></div>';
                strOp += '<div class="col-md-1"><input type="text" class="form-control TextEdit spanStyleMitraSmall" id="txtTool' + id + '" value="' + row.tool + '"/></div>';
                strOp += '<div class="col-md-1"><input type="text" class="form-control TextEdit spanStyleMitraSmall" id="txtArz' + id + '" value="' + row.arz + '"/></div>';
                strOp += '<div class="col-md-1"><input type="text" class="form-control TextEdit spanStyleMitraSmall" id="txtErtefa' + id + '" value="' + row.ertefa + '"/></div>';
                strOp += '<div class="col-md-1"><input type="text" class="form-control TextEdit spanStyleMitraSmall" id="txtVazn' + id + '" value="' + row.vazn + '"/></div>';
                strOp += '<div class="col-md-1 "><label class="RMMJozStyle">' + (row.meghdarJoz === 0 ? "" : Math.round(row.meghdarJoz * 100) / 100) + '</label></div>';
                strOp += '<div class="col-md-2"><input type="text" class="form-control TextEdit spanStyleMitraSmall" id="txtDes' + id + '" value="' + row.des + '"/></div>';
                strOp += '<div class="col-md-1">';
                //strOp += '<i class="fa fa-edit EditRMUStyle displayNone" id="iEdit' + id + '" onclick="EditNRMUClick(\'' + id + '\', \'' + OperationId + '\', \'' + FBId + '\', \'' + lstItemsFields + '\')"></i>';
                strOp += '<i id="iUpdate' + id + '" class="fa fa-save SaveRMUStyle" onclick="UpdateNRMUClick(\'' + id + '\', \'' + OperationId + '\', \'' + FBId + '\')"></i>';
                strOp += '<i class="fa fa-trash DelRMUStyle" onclick="DeleteRMUNClick(\'' + id + '\', \'' + FBId + '\')"></i>';
                strOp += '</div>';
                strOp += '</div>';
            });


            //if (IsFromAddedOperation == 'false') {
            //    $('#PopupViewNextRutin').find('#uldiva' + OperationId).html(strOp);
            //}
            //else
            $('#PopupViewNextRutin').find('#nuldivna' + OperationId).html(strOp);
            // درج به داخل یک عنصر HTML

        },
        error: function (msg) {
            toastr.error('مشکل در دریافت ریزه متره', 'خطا');
        }
    });
}
////////////
function DeleteRMUClick(id) {

    NoeFB = parseInt($('#HDFNoeFB').val());
    Year = parseInt($('#HDFYear').val());
    BarAvordUserId = $('#HDFBarAvordUserID').val();
    FBId = $('#HDFFBID').val();
    OperationId = $('#HDFOperationId').val();


    var vardata = new Object();
    vardata.Id = id;
    vardata.NoeFB = NoeFB;
    vardata.BarAvordUserId = BarAvordUserId;
    vardata.FBId = FBId;
    vardata.OperationId = OperationId;
    vardata.Year = Year;

    $.ajax({
        url: "/RizMetreUser/DeleteRizMetre",
        method: "POST",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            info = response.split('_');
            if (info[0] == "OK") {
                toastr.success('ریز متره بدرستی حذف گردید', 'موفقیت');

                $("#divItemsAddedAndRel" + NoeFB + "_" + info[1] + " div[id^='divShowRizMetre']").each(function () {

                    if ($(this).css("display") === "block") {

                        $(this).slideUp();

                    }
                });

                GetRizMetreWithFBId(1);
            }
        },
        error: function () {
            toastr.error('خطا در حذف ریزمتره!', 'خطا');
        }
    });
}

function DeleteRMUNClick(id, FBId) {
    var vardata = new Object();
    vardata.Id = id;
    $.ajax({
        url: "/RizMetreUser/DeleteRizMetre",
        method: "POST",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response == "OK") {
                toastr.success('ریز متره بدرستی حذف گردید', 'موفقیت');
                GetRizMetreN(FBId);
            }
        },
        error: function () {
            toastr.error('خطا در حذف ریزمتره!', 'خطا');
        }
    });
}

function EditRMUClick(id, OperationId, FBId, Array) {


    $('#HDFOperationId').val(OperationId);
    $('#HDFFBID').val(FBId);
    ArraySplit = Array.split(',');
    i = 0;

    var $row = $("#iEdit" + id).closest(".styleRowTable");
    //$('#iEdit' + id).parent().parent().find('input[Type=text]').each(function () {
    $row.find("input").each(function () {


        $(this).addClass('TextEdit');

        if (i >= 1 && i < 6) {
            if (ArraySplit[(i - 1)] == "True") {
                $(this).removeAttr('disabled');
            }
        }
        else
            $(this).removeAttr('disabled');

        ArraySplit[(i - 1)] == "True" ? $(this).addClass('HasEnteringValue') : '';
        i++;
        $(this).click(function () {
            if (!($(this).is(':focus')))
                $(this).select();
        });
        $(this).focus(function () {
            $(this).select();
        });

        $(this).on("keypress", function (e) {
            /* ENTER PRESSED*/
            if (e.keyCode == 13) {
                /* FOCUS ELEMENT */
                var inputs = $(this).parents().parents().eq(0).find(":input");
                var idx = inputs.index(this);

                if (idx == inputs.length - 1) {
                    inputs[0].select()
                } else {
                    inputs[idx + 1].focus(); //  handles submit buttons
                    inputs[idx + 1].select();
                }
                return false;
            }
        });


    });
    $('#iEdit' + id).hide();
    $('#iUpdate' + id).show();
}
////////////
function EditNRMUClick(id, OperationId, FBId, Array) {
    $('#HDFOperationIdN').val(OperationId);
    $('#HDFFBID').val(FBId);
    ArraySplit = Array.split(',');
    i = 0;

    $('#iEdit' + id).parent().parent().find('input[Type=text]').each(function () {
        $(this).addClass('TextEdit');

        if (i >= 1 && i < 6) {
            if (ArraySplit[(i - 1)] == "True") {
                $(this).removeAttr('disabled');
            }
        }
        else
            $(this).removeAttr('disabled');

        ArraySplit[(i - 1)] == "True" ? $(this).addClass('HasEnteringValue') : '';
        i++;
        $(this).click(function () {
            if (!($(this).is(':focus')))
                $(this).select();
        });
        $(this).focus(function () {
            $(this).select();
        });

        $(this).on("keypress", function (e) {
            /* ENTER PRESSED*/
            if (e.keyCode == 13) {
                /* FOCUS ELEMENT */
                var inputs = $(this).parents().parents().eq(0).find(":input");
                var idx = inputs.index(this);

                if (idx == inputs.length - 1) {
                    inputs[0].focus()
                    inputs[0].select()
                } else {
                    inputs[idx + 1].focus(); //  handles submit buttons
                    inputs[idx + 1].select();
                }
                return false;
            }
        });


    });
    $('#iEdit' + id).addClass('displayNone');
    $('#iUpdate' + id).removeClass('displayNone');
}

function EditRutinCall() {
    $(this).on("keypress", function (e) {
        if (e.keyCode == 13) {
            var inputs = $(this).parent().parent().find("input[Type=text],button");
            var idx = inputs.index(this);
            if (idx == inputs.length - 1) {
                inputs[0].focus();
                inputs[0].select();
            } else {
                while (inputs[idx + 1].disabled == true) {
                    idx++;
                }
                inputs[idx + 1].focus(); //  handles submit buttons
                inputs[idx + 1].select();
            }
            return false;
        }
    });
}

//function AddRizMetreUsersN(OperationId, FBId, Array, LevelNumber) {
//    ArraySplit = Array.split(',');
//    $('#HDFOperationIdN').val(OperationId);
//    $('#HDFFBID').val(FBId);
//    $('#HDFFBID').val(FBId);
//    $('#HDFIsFromAddedOperation').val('true');
//    $('.NoRizMetre').addClass('displayNone');
//    var str = '';
//    str += "<div id=\"divNewRow\" class=\"row styleRowTable\"><div class=\"col-md-1\"><i class=\"fa fa-plus SaveRMUStyle\"></i></div>";
//    str += "<div class=\"col-md-2\"><input type=\"text\" autofocus class=\"form-control spanStyleMitraSmall\"  id=\"txtSharh\" value=\"\"/></div>";
//    str += "<div class=\"col-md-1\"><input type=\"text\"" + (ArraySplit[0] != "True" ? "disabled=\"disabled\" class=\"form-control spanStyleMitraSmall\"" : " class=\"form-control spanStyleMitraSmall HasEnteringValue\"") + " id=\"txtTedad\" value=\"\"/></div>";
//    str += " <div class=\"col-md-1\"><input type=\"text\"" + (ArraySplit[1] != "True" ? "disabled=\"disabled\" class=\"form-control spanStyleMitraSmall\"" : " class=\"form-control spanStyleMitraSmall HasEnteringValue\"") + " id=\"txtTool\" value=\"\"/></div>";
//    str += "<div class=\"col-md-1\"><input type=\"text\"" + (ArraySplit[2] != "True" ? "disabled=\"disabled\" class=\"form-control spanStyleMitraSmall\"" : " class=\"form-control spanStyleMitraSmall HasEnteringValue\"") + " id=\"txtArz\" value=\"\"/></div>";
//    str += "<div class=\"col-md-1\"><input type=\"text\"" + (ArraySplit[3] != "True" ? "disabled=\"disabled\" class=\"form-control spanStyleMitraSmall\"" : " class=\"form-control spanStyleMitraSmall HasEnteringValue\"") + " id=\"txtErtefa\" value=\"\"/></div>";
//    str += "<div class=\"col-md-1\"><input type=\"text\"" + (ArraySplit[4] != "True" ? "disabled=\"disabled\" class=\"form-control spanStyleMitraSmall\"" : " class=\"form-control spanStyleMitraSmall HasEnteringValue\"") + " id=\"txtVazn\" value=\"\"/></div>";
//    str += "<div class=\"col-md-1\">0</div>";
//    str += "<div class=\"col-md-2\"><input type=\"text\" class=\"form-control spanStyleMitraSmall\" id=\"txtDes\" value=\"\"/></div>";
//    str += "<div class=\"col-md-1\"><button type=\"button\" onclick=\"SaveRMUClick($(this),'" + FBId + "','true','" + OperationId + "'," + LevelNumber + ")\" class=\"ButtonRowsSaveStyle\"><i id=\"iSave\" class=\"fa fa-save SaveRMUStyle\"></i></button></div></div>";
//    $('#nuldivna' + OperationId).append(str);

//    $('#nuldivna' + OperationId + ' #divNewRow').find('input[type=text]').each(function () {
//        $(this).addClass('TextEdit');

//        $(this).click(function () {
//            if (!($(this).is(':focus')))
//                $(this).select();
//        });

//        $(this).focus(function () {
//            $(this).select();
//        });

//        $(this).on("keypress", function (e) {
//            /* ENTER PRESSED*/
//            if (e.keyCode == 13) {
//                /* FOCUS ELEMENT */
//                var inputs = $(this).parent().parent().find("input[Type=text],button");
//                var idx = inputs.index(this);
//                if (idx == inputs.length - 1) {
//                    inputs[0].focus();
//                    inputs[0].select();
//                } else {
//                    while (inputs[idx + 1].disabled == true) {
//                        idx++;
//                    }
//                    inputs[idx + 1].focus(); //  handles submit buttons
//                    inputs[idx + 1].select();
//                }
//                return false;
//            }
//        });
//    })
//}

function ShowForms(divName, OPId, LevelNumber) {

    $('btnAddedItems' + OPId).addClass('ActiveAddRelItemTab');
    $('#HDFCurrentTabOpen').val(divName);
    if (LevelNumber == 1) {
        $('#' + divName + OPId).toggle(1000);
        if (divName == 'divAddedItems') {
            $('#divRelItems' + OPId).slideUp(1000);
            //$('#btnRelItems' + OPId).css('background-color', 'rgb(129, 173, 201)');
            $('#btnRelItems' + OPId).removeClass('ActiveAddRelItemTab');
            //$('#btnAddedItems' + OPId).css('background-color', 'rgb(46, 112, 155)');
            $('#btnAddedItems' + OPId).addClass('ActiveAddRelItemTab');
        }
        else if (divName == 'divRelItems') {
            $('#divAddedItems' + OPId).slideUp(1000);
            //$('#btnAddedItems' + OPId).css('background-color', 'rgb(129, 173, 201)');
            $('#btnAddedItems' + OPId).removeClass('ActiveAddRelItemTab');
            //$('#btnRelItems' + OPId).css('background-color', 'rgb(46, 112, 155)');
            $('#btnRelItems' + OPId).addClass('ActiveAddRelItemTab');
        }
    }
    else if (LevelNumber == 2) {
        $('#PopupViewNextRutinTreeView').find('#' + divName + OPId).slideDown(1000);
        if (divName == 'divAddedItems') {
            $('#PopupViewNextRutinTreeView').find('#divRelItems' + OPId).slideUp(1000);
            //$('#PopupViewNextRutinTreeView').find('#btnRelItems' + OPId).css('background-color', 'rgb(129, 173, 201)');
            $('#PopupViewNextRutinTreeView').find('#divRelItems' + OPId).removeClass('ActiveAddRelItemTab');
            //$('#PopupViewNextRutinTreeView').find('#btnAddedItems' + OPId).css('background-color', 'rgb(46, 112, 155)');
            $('#PopupViewNextRutinTreeView').find('#btnAddedItems' + OPId).addClass('ActiveAddRelItemTab');
        }
        else if (divName == 'divRelItems') {
            $('#PopupViewNextRutinTreeView').find('#divAddedItems' + OPId).slideUp(1000);
            //$('#PopupViewNextRutinTreeView').find('#btnAddedItems' + OPId).css('background-color', 'rgb(129, 173, 201)');
            $('#PopupViewNextRutinTreeView').find('#btnAddedItems' + OPId).removeClass('ActiveAddRelItemTab');
            //$('#PopupViewNextRutinTreeView').find('#btnRelItems' + OPId).css('background-color', 'rgb(46, 112, 155)');
            $('#PopupViewNextRutinTreeView').find('#divRelItems' + OPId).addClass('ActiveAddRelItemTab');
        }
    }
}



function RizeshBardariClick(OpId) {
    BarAvordUserID = $('#HDFBarAvordUserID').val();
    RizeshBardariWithBarAvordClick(OpId, BarAvordUserID);
}
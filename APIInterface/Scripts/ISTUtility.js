

///////////////////////////////////// INDEX PAGE

// Form validatoni general
function validateForm() {
    var listSimple = $('.validateMandatory');
    var counter = 0;
    for (var i = 0; i < listSimple.length; i++) {
        var control = listSimple[i];
        var fieldvalue = $(control).val();
        var placeholder = $(control).attr('data-value');
        if (fieldvalue == null || fieldvalue == "" || fieldvalue == placeholder) {
            //$(control).prop('placeholder', $(control).prop('placeholder'));
            counter++;
            $(control).addClass("Error");
            //implementing scroll back to error
        } else {
            $(control).removeClass("Error");
        }
    }
    if (counter > 0) {
        //implementing focus back to error
        var divID = $(".Error")[0].id;
        if ($("#" + divID).length > 0)
            $("#" + divID).focus();
        toastr.error("Please complete the mandatory fields");
        return false;
    }
    return true;
}

// Reservation form validation 
function ValidateReservationForm() {
    $('#DropoffHours').val($('#dropoffForm').val());
    $('#PickupHours').val($('#pickUpForm').val());
    if ($("#ReservationForm_PickupLocation").val() == "") {
        toastr.error("Set Pick-up Location!");
        return false;
    }
    if ($("#ReservationForm_DropoffLocation").val() == "") {
        toastr.error("Set Drop-off Location!");
        return false;
    }

    var pickdate = new Date($("#formSearchUpDate").val());
    var pk = pickdate.getDate() + "/"
                    + (pickdate.getMonth() + 1) + "/"
                    + pickdate.getFullYear();

    var dropDate = new Date($("#formSearchOffDate").val());
    var dk = dropDate.getDate() + "/"
                   + (dropDate.getMonth() + 1) + "/"
                   + dropDate.getFullYear();

    var date = new Date();
    var currentDate = date.getDate() + "/"
                    + (date.getMonth() + 1) + "/"
                    + date.getFullYear();

    if (dk < currentDate || pk < currentDate) {
        //toastr.error("Reservation can not be in the past!");
        //return false;
    }

    if (pk > dk) {
        //toastr.error("Invalid date(s)!");
        //return false;
    }
    return validateForm();
}

// Email from Index
function SendEmailToUser() {
    var senderName = $("#name").val();
    var senderEmail = $("#email").val();
    var emailSubject = $("#subject").val();
    var emailBody = $("#body").val();

    if (senderName == "" || senderEmail == "" || emailSubject == "" || emailBody == "") {
        toastr.error("Field(s) are mandatory!");
        return false;
    }
    var email = {
        SenderName: senderName ,
        SenderEmail: senderEmail,
        EmailSubject: emailSubject,
        EmailBody: emailBody
    };
   
    $('#sendingEmailLabel').css({ 'display': 'block' });
    $('#sendingEmailLabel').text("Sending email...");


    $.ajax({
        type: 'POST',
        data: JSON.stringify(email),
        contentType: "application/json; charset=utf-8",
        url: 'Rental/SendEmail',
        dataType: 'json',
        success: function (response) {
           
            if (response.status == "ok") {
                $('#sendingEmailLabel').text("Email sent!");
                $('#sendingEmailLabel').css({ 'color': 'green' });
                toastr.success("Email sent!");
            } else {
                $('#sendingEmailLabel').css({ 'color': 'red' });
                $('#sendingEmailLabel').text("Email sending failed!");
                toastr.error("Failed to send email.Try agian later!");
            }
        },
        error: function () {
            toastr.error("Failed to send email.Try agian later!");
        },
    });
}

// Index Loading 
function IdontKnow() {
    jQuery(window).load(function () { jQuery('body').scrollspy({ offset: 100, target: '.navigation' }); });
    jQuery(window).load(function () { jQuery('body').scrollspy('refresh'); });
    jQuery(window).resize(function () { jQuery('body').scrollspy('refresh'); });
    jQuery(window).load(function () {
        if (location.hash != '') {
            var hash = '#' + window.location.hash.substr(1);
            if (hash.length) {
                jQuery('html,body').delay(0).animate({
                    scrollTop: jQuery(hash).offset().top - 44 + 'px'
                }, {
                    duration: 1200,
                    easing: "easeInOutExpo"
                });
            }
        }
    });
}


///////////////////////////////// SELECT CAR PAGE

// Hire Group Accor
function accordionSetting() {
    $(function () {
        $("#accordion").accordion({
            collapsible: true,
            active: false,
            heightStyle: "content",
            icons: {
                "header": "ui-icon-plus",
                "activeHeader": "ui-icon-minus"
            }
        });
    });
}

// Vehicle Selection submition
$('.Select').click(function () {
    hideUi();
    var id = this.id.substring(9, this.id.length);
    if (document.getElementById("TotalC_" + id).innerHTML == '(No Standard Rate)') {
        $.growlUI('', 'Calculate Charge First!');
        return false;
    }
    window.location = "SelectExtras?hireGroupDetailId=" + id;

});

// Calculate Charge
$('.Charge').click(function () {
    var id = this.id.substring(9, this.id.length);
    if (document.getElementById("TotalC_" + id).innerHTML != ' ') {
        return false;
    }
    hideUi();

    $.ajax({
        type: 'POST',
        data: "{'hireGroupDetailId':'" + id + "'}",
        contentType: "application/json; charset=utf-8",
        url: 'CalculateCharge',
        dataType: 'json',
        success: function (response) {
            // document.getElementById(id).disabled = false;
            if (response.detail !== null) {
                showUi();
                if (response.hGcharge.TariffTypeCode == null) {
                    alert("Standard rates are not defined for current tarrif type!");
                    return;
                }
                var total = numeral(response.hGcharge.TotalStandardCharge).format('0,0');
                var perDay = numeral(response.hGcharge.PerDayCost).format('0,0');
                document.getElementById("TotalC_" + id).innerHTML = ("SAR " + total);
                document.getElementById("perdaycost_" + id).innerHTML = ("(SAR " + perDay + "/day)");
                $("#selectId_" + id).show();
                $("#chargeId_" + id).css('display', 'none');



                $("#TotalC_" + id).pulsate({
                    color: "#e80c4d",
                    reach: 50,
                    repeat: 5,
                    speed: 800,
                    glow: true
                });
            }
            else {

            }

        },
        error: function () {

        },
    });
});

// Navigation Bar | Back to Reservation Form 
$("#startingForm").hover(function () {
    var cls = document.getElementById("startingForm").className;
    if (cls == "progress-bar-step done") {
        $("#startingForm").removeClass("progress-bar-step done").addClass("progress-bar-step current");
        return false;
    }
    $("#startingForm").removeClass("progress-bar-step current").addClass("progress-bar-step done");

});


//////////////////////////// SEELCT EXTRAS PAGE
var extrasList = new Array();
var insurancesList = new Array();

// Extra's Page Submition 
$("form").submit(function () {
    var index;
    var text = '-99';
    for (index = 0; index < extrasList.length; index++) {
        text = extrasList[index] + "," + text;
    }
    $("#ServiceItemsIds").val(text);
    text = '-99';
    for (index = 0; index < insurancesList.length; index++) {
        text = insurancesList[index] + "," + text;
    }
    $("#InsurancesIds").val(text);
});

// Get Insurances Rate
function callInsurance() {
    var Id = this.id;
    if (document.getElementById("rate_" + Id).innerHTML != '(Select Item)') {
        var valueToMinus = document.getElementById("rate_" + Id).textContent;
        document.getElementById("rate_" + Id).innerHTML = '(Select Item)';
        $("#currency_" + Id).hide();

        var list = document.getElementById('extras_Total_Sidebar');
        var entryToBeRemoved = document.getElementById("ins_" + Id);
        list.removeChild(entryToBeRemoved);
        if ($("#extras_Total_Sidebar").children().length == 1) {
            $('#defaultItem').show();
        }
        var prevTotal = document.getElementById('overAllTotal').textContent;
        var unformatedTotal = numeral().unformat(prevTotal);
        var unformatedValueToMinus = numeral().unformat(valueToMinus);
        var newTotal = unformatedTotal - unformatedValueToMinus;

        document.getElementById('overAllTotal').textContent = numeral(newTotal).format('0,0');
        var index = insurancesList.indexOf(Id);
        if (index > -1) {
            insurancesList.splice(index, 1);
        }
        return true;
    }
    hideUi();
    $.ajax({
        type: 'POST',
        data: "{'insuranceTypeId':'" + Id + "'}",
        contentType: "application/json; charset=utf-8",
        url: 'GetChargeForInsuranceType',
        dataType: 'json',
        success: function (response) {
            if (response) {
                document.getElementById("rate_" + Id).innerHTML = numeral(response.insuranceCharge.Charge).format('0,0');
                list = document.getElementById('extras_Total_Sidebar');
                var itemName = document.getElementById('ItemName_' + Id).textContent;
                $("#currency_" + Id).show();

                var newdiv = document.createElement('p');
                newdiv.setAttribute('id', 'ins_' + Id);
                newdiv.innerHTML = itemName + ' <span class="price">' + "SAR " + numeral(response.insuranceCharge.Charge).format('0,0') + '</span>';
                list.appendChild(newdiv);

                //  var newTotal =  response.itemCharge.ServiceCharge;
                prevTotal = document.getElementById('overAllTotal').textContent;
                unformatedTotal = numeral().unformat(prevTotal);

                document.getElementById('overAllTotal').textContent = numeral(unformatedTotal + response.insuranceCharge.Charge).format('0,0');
                insurancesList.push(Id);
                toastr.success("Item added!");
                $('#defaultItem').hide();
            }
            else {

            }
            showUi();
        },
        error: function () {
            //showUi();
        },
    });
}

// Gets Rate for Service Item
function callExtra() {

    var Id = this.id;
    if (document.getElementById("rate_" + Id).innerHTML != '(Select Item)') {
        var valueToMinus = document.getElementById("rate_" + Id).textContent;
        document.getElementById("rate_" + Id).innerHTML = '(Select Item)';
        $("#currency_" + Id).hide();

        var list = document.getElementById('extras_Total_Sidebar');
        var entryToBeRemoved = document.getElementById("exs_" + Id);
        list.removeChild(entryToBeRemoved);
        if ($("#extras_Total_Sidebar").children().length == 1) {
            $('#defaultItem').show();
        }


        // here
        var prevTotal = document.getElementById('overAllTotal').textContent;
        var unformatedTotal = numeral().unformat(prevTotal);
        var unformatedValueToMinus = numeral().unformat(valueToMinus);
        var newTotal = unformatedTotal - unformatedValueToMinus;

        document.getElementById('overAllTotal').textContent = numeral(newTotal).format('0,0');

        var index = extrasList.indexOf(Id);
        if (index > -1) {
            extrasList.splice(index, 1);
        }
        return true;
    }
    hideUi();
    $.ajax({
        type: 'POST',
        data: "{'serviceItemId':'" + Id + "','quantity':'1'}",
        contentType: "application/json; charset=utf-8",
        url: 'GetChargeForServiceItem',
        dataType: 'json',
        success: function (response) {
            if (response) {
                document.getElementById("rate_" + Id).innerHTML = numeral(response.itemCharge.ServiceCharge).format('0,0');
                list = document.getElementById('extras_Total_Sidebar');
                var itemName = document.getElementById('ItemName_' + Id).textContent;
                $("#currency_" + Id).show();

                var newdiv = document.createElement('p');
                newdiv.setAttribute('id', 'exs_' + Id);
                newdiv.innerHTML = itemName + ' <span class="price">' + "SAR " + numeral(response.itemCharge.ServiceCharge).format('0,0') + '</span>';
                list.appendChild(newdiv);


                //  var newTotal =  response.itemCharge.ServiceCharge;
                prevTotal = document.getElementById('overAllTotal').textContent;
                unformatedTotal = numeral().unformat(prevTotal);

                document.getElementById('overAllTotal').textContent = numeral(unformatedTotal + response.itemCharge.ServiceCharge).format('0,0');


                extrasList.push(Id);
                $('#defaultItem').hide();
                toastr.success("Item added!");
            }
            else {

            }
            showUi();
        },
        error: function () {
            showUi();
        },
    });
}

///////////////////////// CHECK OUT PAGE
function IntitCheckout() {
    var d = new Date();
    var year = d.getFullYear() - 20;

    $("#dob_value").datepicker({
        changeMonth: true,
        changeYear: true,
        yearRange: '1920:' + year,
        defaultDate: '01/01/1990'
    });
}

// Final Booking FOrm 
function validateBookingForm() {
    $("#DOB").val($("#dob_value").val());
    if (DOBCheck())
        return validateForm();
    else
        return false;
}

// DAte Check for usr >21
function DOBCheck() {
    var today = new Date();
    var birthDate = new Date($("#dob_value").val());
    var age = today.getFullYear() - birthDate.getFullYear();
    var m = today.getMonth() - birthDate.getMonth();
    if (isNaN(age) || age < 21) {
        toastr.error("Age must be greater that 21 years!");
        return false;
    }
    return true;
}
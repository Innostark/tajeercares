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
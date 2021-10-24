function submitSignUp() {
    var _user = $("#user").val();
    var _pass = $("#pass").val();
    var _repass = $("#re-pass").val();
    var _email = $("#email").val();

    var valid = validateSignUp();

    if (valid == false) {
        return false
    }

    $.ajax({
        url: "/Register/SaveMember",
        type: "POST",
        data: {
            UserName: _user,
            PassWord: _pass,
            Email: _email
        },
        success: function (rs) {
            $("#frmSignUp")[0].reset();
            $("#SiginModal").modal("hide");
        },
        error: function (ex) {
            alert("fail!");
        }
    });
};

//Function for clearing the textboxes  
function clearTextBox() {
    $('#username').val("");
    $('#password').val("");
    $('#user').val("");
    $('#pass').val("");
    $('#re-pass').val("");
    $('#email').val("");
    $('#username').css('border', 'none');
    $('#password').css('border', 'none');
    $('#user').css('border', 'none');
    $('#pass').css('border', 'none');
    $('#re-pass').css('border', 'none');
    $('#email').css('border', 'none');
}
//Valdidation using jquery  
function validateSignUp() {
    var isValid = true;
    if ($('#user').val().trim() == "") {
        $('#user').css('border', '1px solid red');
        isValid = false;
    }
    else {
        $('#user').css('border', 'lightgrey');
    }
    if ($('#pass').val().trim() == "" || $('#re-pass').val().trim() == "" || $('#pass').val().trim() != $('#re-pass').val().trim()) {
        $('#pass').css('border', '1px solid red');
        $('#re-pass').css('border', '1px solid red');
        isValid = false;
    }
    else {
        $('#pass').css('border', 'lightgrey');
        $('#re-pass').css('border', 'lightgrey');
    }

    if ($('#email').val().trim() == "") {
        $('#email').css('border', '1px solid red');
        isValid = false;
    }
    else {
        $('#email').css('border', 'lightgrey');
    }
    
    return isValid;
}
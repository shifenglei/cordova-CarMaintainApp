(function () {
    $(function () {
        if ($("#errMsg").val() != undefined && $("#errMsg").val().length > 0) {
            alert($("#errMsg").val());

            $.cookie("rememberUser", "false", { expries: -1 });
            $.cookie("UserCode", "", { expries: -1 });
            $.cookie("Password", "", { expries: -1 });
        }
        if ($.cookie("rememberUser") == "true") {            
            $("#cbRemember").attr("checked", "checked");
            $("#txtUserCode").val($.cookie("UserCode"));
            $("#txtPwd").val($.cookie("Password"));
        }
    })
    $("#btnLogin").click(function () {
        $("#errMsg").val("");
        if ($("#cbRemember").attr("checked") == "checked") {
            $.cookie("rememberUser", "true", { expries: 15 });
            $.cookie("UserCode", $("#txtUserCode").val(), { expries: 15 });
            $.cookie("Password", "", { expries: 15 });            
        }
        /*$.post("/UserInfo/ULogin",
            {"UserCode":$("#txtUserCode").val(),"Password":$("#txtPwd").val()},
            function (data, status) {
                alert(data);
                if (data.code == -1)
                    alert(data.errmsg);
            }
        );*/
    });
})();
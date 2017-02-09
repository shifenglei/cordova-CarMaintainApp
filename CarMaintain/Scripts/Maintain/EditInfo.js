(function () {
    var submitEditInfo = function () {
        $.post("/Maintain/EditTask",
            $("#frmEditInfo").serialize(),
            function (data) {
                try{
                    var jsondata = JSON.parse(data);
                }
                catch (e){
                    alert(e.message);
                }
                if (jsondata == null || jsondata == undefined) {
                    alert("返回值错误");
                };

                if (jsondata.status == 1) {
                    alert("提交成功");
                    window.location.href = "/MainMenu/Index";
                }
                else {
                    alert(jsondata.ErrMsg);
                }
            })
            ;
    }
    $("#btnSubmit").click(submitEditInfo);
})()
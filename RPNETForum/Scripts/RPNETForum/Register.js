var RPNETForum;
(function (RPNETForum) {
    var FormInput = RPNETForum.Controls.FormInput;
    var Register = (function () {
        function Register() {
            var submission = {
                username: "",
                password: "",
                confirmPassword: "",
                email: ""
            };
            var inputs = {};
            inputs["username"] = new FormInput($("<input>").addClass("form-control"), "Username");
            inputs["password"] = new FormInput($("<input>").addClass("form-control"), "Password");
            inputs["confirmPassword"] = new FormInput($("<input>").addClass("form-control"), "Confirm Password");
            inputs["email"] = new FormInput($("<input>").addClass("form-control"), "Email");
            for (var item in inputs) {
                if (inputs.hasOwnProperty(item)) {
                    var input = inputs[item];
                    var row = $("<div>").addClass("row").append($("<div>").addClass("col-xs-3 control-label").append(input.display), $("<div>").addClass("col-xs-9").append(input.container));
                    $(".well").append(row);
                }
            }
            var submit = $("<button>").addClass("btn btn-success pull-right").text("Submit").on("click", function () {
                for (var item in inputs) {
                    if (inputs.hasOwnProperty(item)) {
                        var input = inputs[item];
                        submission[item] = input.input.val();
                    }
                }
                $.ajax({
                    url: "/Register/Submit",
                    type: "POST",
                    dataType: "json",
                    data: submission,
                    success: function (data) {
                        for (var item in data) {
                            if (inputs.hasOwnProperty(item.toLowerCase()) && data.hasOwnProperty(item)) {
                                var input = inputs[item.toLowerCase()];
                                var currentData = data[item];
                                console.log(currentData);
                                input.container.removeClass("has-error");
                                input.container.removeClass("has-success");
                                input.label.text("");
                                if (currentData.Success) {
                                    input.container.addClass("has-success");
                                }
                                else {
                                    input.container.addClass("has-error");
                                    input.label.html(currentData.Reason);
                                    console.log(currentData.Reason);
                                }
                            }
                        }
                    }
                });
            });
            $(".well").append(submit, "<br/><br/>");
        }
        return Register;
    }());
    $(document).ready(function () {
        // ReSharper disable once WrongExpressionStatement
        new Register();
    });
})(RPNETForum || (RPNETForum = {}));
//# sourceMappingURL=Register.js.map
var RPNETForum;
(function (RPNETForum) {
    var Controls;
    (function (Controls) {
        var FormInput = (function () {
            function FormInput(input, display) {
                this.input = input;
                this.display = display;
                this.container = $("<div>").addClass("form-group");
                this.label = $("<span>").addClass("control-label");
                this.container.append(this.input, this.label);
            }
            return FormInput;
        }());
        Controls.FormInput = FormInput;
    })(Controls = RPNETForum.Controls || (RPNETForum.Controls = {}));
})(RPNETForum || (RPNETForum = {}));

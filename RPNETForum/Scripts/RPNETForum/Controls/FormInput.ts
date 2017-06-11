namespace RPNETForum.Controls {
    export class FormInput {

        container: JQuery;

        label: JQuery;

        input: JQuery;

        display: string;

        constructor(input: JQuery, display: string) {
            this.input = input;

            this.display = display;

            this.container = $("<div>").addClass("form-group");

            this.label = $("<span>").addClass("control-label");

            this.container.append(this.input, this.label);
        }
    }
}
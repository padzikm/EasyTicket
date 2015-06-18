$(function () {

    // Don't allow browser caching of forms
    $.ajaxSetup({ cache: false });

    // Wire up the click event of any current or future dialog links
    $('.dialogLink').click(function () {
        var element = $(this);

        // Retrieve values from the HTML5 data attributes of the link        
        var dialogTitle = element.attr('data-dialog-title');
        var updateTargetId = '#' + element.attr('data-update-target-id');
        var updateUrl = element.attr('data-update-url');

        // Generate a unique id for the dialog div
        var dialogId = 'uniqueName-' + Math.floor(Math.random() * 1000)
        var dialogDiv = "<div id='" + dialogId + "'></div>";

        // Load the form into the dialog div
        $(dialogDiv).load(this.href, function () {
            $(this).dialog({
                width: 350,
                modal: true,
                resizable: false,
                title: dialogTitle,
                
                buttons: {
                    "Save": function () {
                        // Manually submit the form                        
                        var form = $('form', this);
                        $(form).submit();
                    },
                    "Cancel": function () { $(this).dialog('close'); }
                }
            });

            $('.datepicker').datepicker();

            $.validator.unobtrusive.parse(this);

            wireUpForm(this, updateTargetId, updateUrl);
        });
        return false;
    });
});

function wireUpForm(dialog, updateTargetId, updateUrl) {
    $('form', dialog).submit(function () {

        if (!$(this).valid())
            return false;
        $("div.loader").css("display", "normal");
        $.ajax({
            url: this.action,
            type: this.method,
            data: $(this).serialize(),
            success: function (result) {
                if (result.success) {
                    $("div.loader").css("display", "none");
                    $(dialog).dialog('close');
                    if (result.login != "") {
                        $("#logins").prepend("<label>" + result.login + "</label>");
                    }
                    //$(updateTargetId).load(updateUrl);
                } else {                  
                    $(dialog).html(result);

                    $.validator.unobtrusive.parse(dialog);

                    wireUpForm(dialog, updateTargetId, updateUrl);
                }
            }
        });
        return false;
    });
}
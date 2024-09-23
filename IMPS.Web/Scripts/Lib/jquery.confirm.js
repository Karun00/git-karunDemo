(function ($) {

    $.confirm = function (params) {

        if ($('#confirmOverlay').length) {
            // A confirm is already shown on the page:
            return false;
        }

        var buttonHTML = '';
        $.each(params.buttons, function (name, obj) {

            // Generating the markup for the buttons:

            buttonHTML += '<a href="#" class="btn green button ' + obj['class'] + '">' + name + '<span></span></a>';

            if (!obj.action) {
                obj.action = function () { };
            }
        });

        var markup = [
            '<div id="confirmOverlay">',
			'<div id="confirmBox" style="padding:10px;">',
            '<button id="clolebt" type="button" class="close" onclick="$.confirm.close()"></button>',
            '<h3>', params.title, '</h3>',
            '<ul class="page-breadcrumb breadcrumb"></ul>',
			'<p>', params.message, '</p>',
			'<div id="confirmButtons">',
			buttonHTML,
			'</div></div></div>'
        ].join('');

        $(markup).hide().appendTo('body').fadeIn();

        var buttons = $('#confirmBox .button'),
			i = 0;

              
        $.each(params.buttons, function (name, obj) {
            buttons.eq(i++).click(function () {

                // Calling the action attribute when a
                // click occurs, and hiding the confirm.

                obj.action();
                $.confirm.hide();
                return false;
            });
        });
    }

    $.confirm.close = function () {        
        $.confirm.hide();
        return false;
    }

    $.confirm.hide = function () {
        $('#confirmOverlay').fadeOut(function () {
            $(this).remove();
        });
    }
}

)(jQuery);
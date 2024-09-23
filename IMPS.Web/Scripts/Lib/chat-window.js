// JavaScript Document
/*
			document ready.
		*/
$(document).ready(function () {
    /*
        declare gloabl box variable,
        so we can check if box is alreay open,
        when user click toggle button
    */
    var box = null;
    var box1 = null;

    /*
        we are now adding click hanlder for 
        toggle button.
    */

    $("#chatbtn1").click(function (event, ui) {
        /*
            now if box is not null,
            we are toggling chat box.
        */
        if (box) {
            /*
                below code will hide the chatbox that 
                is active, when first clicked on toggle button
            */
            box.chatbox("option", "boxManager").toggleBox();

        }
        else {
            /*
                if box variable is null then we will create
                chat-box.
            */
            box = $("#chat_div1").chatbox(
            {
                /*
                    unique id for chat box
                */
                id: "FirstUser",
                user:
                {
                    key: "value"
                },
                /*
                    Title for the chat box
                */
                title: "First User",
                offset: 0,
                /*
                    messageSend as name suggest,
                    this will called when message sent.
                    and for demo we have appended sent message to our log div.
                */
                messageSent: function (id, user, msg) {
                    $("#log").append(id + " said: " + msg + "<br/>");
                    $("#chat_div1").chatbox("option", "boxManager").addMsg(id, msg);
                }
            });
        }
    });

    $("#chatbtn2").click(function (event, ui) {
        /*
            now if box is not null,
            we are toggling chat box.
        */
        if (box1) {
            /*
                below code will hide the chatbox that 
                is active, when first clicked on toggle button
            */
            box1.chatbox1("option", "boxManager").toggleBox();

        }
        else {
            /*
                if box variable is null then we will create
                chat-box.
            */
            box1 = $("#chat_div2").chatbox1(
            {
                /*
                    unique id for chat box
                */
                id: "UserList",
                user:
                {
                    key: "value"
                },
                /*
                    Title for the chat box
                */
                title: "User List",
                offset: 350,
                /*
                    messageSend as name suggest,
                    this will called when message sent.
                    and for demo we have appended sent message to our log div.
                */
                messageSent: function (id, user, msg) {
                    $("#log").append(id + " said: " + msg + "<br/>");
                    $("#chat_div2").chatbox1("option", "boxManager").addMsg(id, msg);
                }
            });
        }
    });

});



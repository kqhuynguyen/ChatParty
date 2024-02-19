"use strict";
var connection = new signalR.HubConnectionBuilder()
    .withUrl("/chatHub")
    .configureLogging(signalR.LogLevel.Debug)
    .build();

//Disable the send button until connection is established.
document.getElementById("sendButton").disabled = true;
connection.on("ReceiveMessage", function (user, message) {

    const template = document.createElement('template');

    template.innerHTML = `<div class="flex flex-row">
		<div class="text-blue-300 text-xs mr-4 shrink-0 flex-grow-0 basis-15">User 1:</div>
		<div class="text-balance text-white-50 text-sm">Lorem ipsum donor salova.</div>
	</div>`
    const div = template.content.cloneNode(true).children[0];
    div.children[0].textContent = `${user}:`;
    div.children[1].textContent = message;
    if (div) {
        document.getElementById("messagesList").appendChild(div);
        document.getElementById("messageInput").value = "";
        // We can assign user-supplied strings to an element's textContent because it
        // is not interpreted as markup. If you're assigning in any other way, you
        // should be aware of possible script injection concerns.
    }
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});


function onSendButtonclick(event) {
    var message = document.getElementById("messageInput").value;
    var params = (new URL(document.location)).searchParams;
    let toId = params.get("id");
    connection.invoke("SendMessage", message, toId).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
}


function onEnter(event) {
    event.preventDefault();
    if (event.keyCode === 13) {
        document.getElementById("sendButton").click();
    }
}

document.getElementById("messageInput")
    .addEventListener("keyup", onEnter);
document.getElementById("sendButton")
    .addEventListener("click", onSendButtonclick);


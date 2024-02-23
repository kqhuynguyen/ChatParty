"use strict"

$("#createGroupButton").click(async () => {
    const addedUsers = []

    addedUsers.push((new URL(document.location)).searchParamsget("id"))
    addedUsers.push(connection.hub.id)

    for (const childElement in $("#addedUserList").children) {
        addedUsers.push(childElement.innerText)
    }

    const data = {
        channel: {
            name: "New Channel"
        },
        userIds: addedUsers
    }

    const response = await fetch("/Channel/Create", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.Stringify(data)
    });

    window.location.href = `/Channel?id=${response.id}`

})
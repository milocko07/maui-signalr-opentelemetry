const connection = new signalR.HubConnectionBuilder()
    .withUrl("https://localhost:7007/counterHub") // Replace with your SignalR hub URL (e.g., http://localhost:5000/hub)
    .build();

connection.on("receiveMessage", (message) => {
    const msgElement = document.createElement("p");
    msgElement.textContent = message;
    document.getElementById("messages").appendChild(msgElement);
});

document.getElementById("connectButton").addEventListener("click", () => {
    connection.start().then(() => {
        console.log("Connected!");
        // Send messages or invoke server methods here
    }).catch(err => {
        debugger;
        console.log(err)});
});
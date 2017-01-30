
function updateBotStatus() {
    var x = new XMLHttpRequest();
    x.open("GET", "/API/GetBotStatus", true);
    x.onload = function () {
        $('#bot_status').removeAttr('class');
        $('#bot_status').addClass("status-" + x.responseText);
    }
    x.send(null);
}

function runAPI(f) {
    var x = new XMLHttpRequest();
    x.open("GET", "/API/" + f, true);
    x.send(null);
}

function startUpdate() {
    var botStatusUpdater = setInterval(function () {
        updateBotStatus();
    }, 1500);
}
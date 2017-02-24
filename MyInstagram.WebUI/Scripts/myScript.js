function suc(data) {
    var a = document.getElementById("subbtn").value;
    if (a == "Unfollow") {
        document.getElementById("subbtn").value = "Follow";
        //alert("yes");
    }
    else {
        document.getElementById("subbtn").value = "Unfollow";
        //alert(data);
    }
}
function fail() {
    alert("fail");
}

function CheckFileSize() {
    var input;
    alert('ASDASD');
    if (!window.FileReader) {
        alert("The file API isn't supported on this browser yet.");
        return;
    }

    input = document.getElementById('profileimageinput');
    if (!input) {
        alert("Um, couldn't find the fileinput element.");
    }

    var elm = document.getElementById('inputfileerror');
    var filesize = input.files[0].size / 1024 / 1024;
    var filetype = input.files[0].type;


    if (filetype === "image/jpeg" || filetype === "image/bmp" || filetype === "image/png") {
        if (filesize > 0.5) {
            elm.innerHTML = "error size > 0.5 MB";
            input.value = "";
        }
        else
            elm.innerHTML = "";
    }
    else {
        elm.innerHTML = "error type! only: .jpeg .bmp .png";
        input.value = "";
    }
    //else
    //if (filesize < 0.5) {
    //    elm.innerHTML = "error size > 0.5 MB" + filetype;
    //    input.value = "";
    //}
    //else
    //    elm.innerHTML = "";
    //document.getElementById('profileimagesbbtn').disabled = false;
}
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
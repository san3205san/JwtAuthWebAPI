function login() {
    debugger;

    var user = $("#loginuser").val();
    var pwd = $("#password").val();
    var url = API_DEV_ENV.toString() + "api/login";


    var login = { Username: user, Password: pwd };

    $.ajax({
        type: "POST",
        url: url,
        data: JSON.stringify(login),
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        success: function (data) {
            alert("login successfully");
            localStorage.setItem("token", data.token);
           console.log("token=" + data.token);
            window.location.replace("/Home/Privacy");
        },
            error: function (error) {
            debugger;
            console.log(error);
        },
    });

   

    //$.ajax({
    //    type: "GET",
    //    url: url,
    //    success: function (data) {
    //        debugger;
    //        localStorage.setItem("token", data.token);
    //        console.log("token=" + data.token);
    //        window.location.replace("/Home/Privacy");
    //    },
    //    error: function (error) {
    //        debugger;
    //        console.log(error);
    //    },
    //    dataType: "json",
    //});



}
function getAll() {
    debugger;
    var obj = "";
    var serviceUrl = API_DEV_ENV.toString() + "weatherforecast"

    getAPI(serviceUrl, onSuccess, onFailled);

    function onSuccess(jsonData) {
        debugger;
        obj = jsonData;
        $.each(jsonData, function (i, item) {
            var rows = "<tr>" +
                "<td id='date'>" + item.date + "</td>" +
                "<td id='temperatureC'>" + item.temperatureC + "</td>" +
                "<td id='temperatureF'>" + item.temperatureF + "</td>" +
                "<td id='summary'>" + item.summary + "</td>" +
                "</tr>";
            $("#tableForcast").append(rows);
        });
    }
    function onFailled(error) {
        window.alert(error.statusText);
    }
    return obj;
}
function getAPI(serviceUrl, successCallback, errorCallBack) {
    $.ajax({
        type: "GET",
        url: serviceUrl,
        dataType: "json",
        success: successCallback,
        error: errorCallBack
    });
}
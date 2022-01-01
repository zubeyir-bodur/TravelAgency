// syncrhonous requests
function AjaxGet(values = null, controllerName, actionUrl) {
    var returning;
    var url = "http://localhost:5000/api/" + controllerName;
    if (!(actionUrl === ""))
        url = "http://localhost:5000/api/" + controllerName + "/" + actionUrl
    console.log(url);
    if (values == null) {
        $.ajax({
            "url": url,
            "method": "GET",
            "async": false,
            "headers": {
                "Authorization": "Bearer "
            },
            "success": (response) => returning = response
        });
    }
    else {
        $.ajax({
            "url": url,
            "async": false,
            "headers": {
                "Authorization": "Bearer "
            },
            "method": "GET",
            "data": values,
            "success": (response) => returning = response
        });
    }
    return returning;
}

function AjaxDelete(values, controllerName, actionUrl) {
    var returning;
    var url = "http://localhost:5000/api/" + controllerName;
    if (!(actionUrl === ""))
        url = "http://localhost:5000/api/" + controllerName + "/" + actionUrl
    $.ajax({
        "url": url,
        "method": "DELETE",
        "async": false,
        "headers": {
            "Content-Type": "application/json",
            "Authorization": "Bearer "
        },
        "data": JSON.stringify(values),
        "dataType": "json",
        "success": (response) => returning = response
    });
    return returning;
}

function AjaxPut(values, controllerName, actionUrl) {
    var returning;
    var url = "http://localhost:5000/api/" + controllerName;
    if (!(actionUrl === ""))
        url = "http://localhost:5000/api/" + controllerName + "/" + actionUrl
    $.ajax({
        "url": url,
        "method": "PUT",
        "async": false,
        "headers": {
            "Content-Type": "application/json",
            "Authorization": "Bearer "
        },
        "data": JSON.stringify(values),
        "dataType": "json",
        "success": (response) => returning = response
    });
    return returning;
}

function AjaxPost(values, controllerName, actionUrl) {
    var url = "http://localhost:5000/api/" + controllerName;
    if (!(actionUrl === ""))
        url = "http://localhost:5000/api/" + controllerName + "/" + actionUrl
    $.ajax({
        "url": url,
        "method": "POST",
        "async": false,
        "headers": {
            "Content-Type": "application/json",
            "Authorization": "Bearer "
        },
        "data": JSON.stringify(values),
        "dataType": "json",
        "success": (response) => returning = response
    });
    return returning;
}

// asynchronous requests
async function AjaxGetAsync(values = null, controllerName, actionUrl) {
    var returning;
    var url = "http://localhost:5000/api/" + controllerName;
    if (!(actionUrl === ""))
        url = "http://localhost:5000/api/" + controllerName + "/" + actionUrl
    if (values == null) {
        await $.ajax({
            "url": url,
            "method": "GET",
            "headers": {
                "Authorization": "Bearer "
            },
            "success": (response) => returning = response
        });
    }
    else {
        await $.ajax({
            "url": url,
            "method": "GET",
            "data": values,
            "headers": {
                "Authorization": "Bearer "
            },
            "success": (response) => returning = response
        });
    }
    return returning;
}

async function AjaxDeleteAsync(values, controllerName, actionUrl) {
    var returning;
    var url = "http://localhost:5000/api/" + controllerName;
    if (!(actionUrl === ""))
        url = "http://localhost:5000/api/" + controllerName + "/" + actionUrl
    await $.ajax({
        "url": url,
        "method": "DELETE",
        "headers": {
            "Content-Type": "application/json",
            "Authorization": "Bearer "
        },
        "data": JSON.stringify(values),
        "dataType": "json",
        "success": (response) => returning = response
    });
    return returning;
}

async function AjaxPutAsync(values, controllerName, actionUrl) {
    var returning;
    var url = "http://localhost:5000/api/" + controllerName;
    if (!(actionUrl === ""))
        url = "http://localhost:5000/api/" + controllerName + "/" + actionUrl
    await $.ajax({
        "url": url,
        "method": "PUT",
        "headers": {
            "Content-Type": "application/json",
            "Authorization": "Bearer "
        },
        "data": JSON.stringify(values),
        "dataType": "json",
        "success": (response) => returning = response
    });
    return returning;
}

async function AjaxPostAsync(values, controllerName, actionUrl) {
    var url = "http://localhost:5000/api/" + controllerName;
    if (!(actionUrl === ""))
        url = "http://localhost:5000/api/" + controllerName + "/" + actionUrl
    await $.ajax({
        "url": url,
        "method": "POST",
        "headers": {
            "Content-Type": "application/json",
            "Authorization": "Bearer "
        },
        "data": JSON.stringify(values),
        "dataType": "json",
        "success": (response) => returning = response
    });
    return returning;
}
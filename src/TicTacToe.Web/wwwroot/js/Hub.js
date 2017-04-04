$(function () {
    var demo = $.connection.demo,
        typedDemo = $.connection.typedDemoHub,
        groupAddedCalled = false;

    typedDemo.client.echo = function (message, invokeCount) {
        $('#typed').append('<p>' + message + ' #' + invokeCount + ' triggered!</p>')
    };

    typedDemo.client.methodB = function (arg1, arg2) {
        $('#typedContext').append('<p>IHubContext<TypedDemoHub, IClient> worked! ' + arg1 + ':' + arg2 + '</p>');
    };

    $.connection.hub.logging = true;

    $.connection.hub.start({ transport: activeTransport }, function () {

        typedDemo.server.echo("Typed echo callback").done(function () {
            $('#typed').append('<p>TypedDemoHub.Echo(string message) invoked!</p>')
        });

    });
});
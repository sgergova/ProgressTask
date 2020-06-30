$().ready(function(){
    $("email").validate({
        rules: {
            email: {
                requred = true,
                email: true
            }
        }
    })
},
messages: {
    email: 
        "Please provide your email"
    
});
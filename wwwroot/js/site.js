function togglePasswordEye() {
    var password = document.getElementById('myInput');
    
    if(password.getAttribute('type') === 'password'){
        password.setAttribute('type', 'text');
        document.getElementById('font').style.color = 'blue';
    }
    else{
        password.setAttribute('type', 'password');
        document.getElementById('font').style.color = 'black';
    }
}
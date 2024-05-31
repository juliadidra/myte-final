function copyEmailAndPassword() {
    /* Obtém os campos de texto */
    var email = document.getElementById("email");
    var password = document.getElementById("password");

    /* Cria uma área de texto temporária para armazenar os valores */
    var tempInput = document.createElement("textarea");
    document.body.appendChild(tempInput);

    /* Define o valor da textarea para o email e senha */
    tempInput.value = "Email: " + email.value + "\nPassword: " + password.value;

    /* Selecione o texto na área de texto */
    tempInput.focus();
    tempInput.setSelectionRange(0, tempInput.value.length);

    /* Copie o texto dentro da textarea */
    document.execCommand("copy");

    /* Remove a área de texto temporária */
    document.body.removeChild(tempInput); 

}




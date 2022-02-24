$.validator.addMethod(
    "ConditionId",
    function (value, element) { return ($("input[name='ConditionId']:checked").val() != undefined); },
    "Choix obligatoire"
);
$.validator.addMethod(
    "GuitarTypeId",
    function (value, element) { return ($("input[name='GuitarTypeId']:checked").val() != undefined); },
    "Choix obligatoire"
);
$("#ImageURL").change(function () {
    $("#preview").css("background", 'url("' + $("#ImageURL").val() + '") no-repeat center ');
    $("#preview").css("background-size", 'contain');
})
$("#RotateImage").change(function () {
    if ($('#RotateImage').is(":checked")) {
        $("#preview").css("transform", 'rotate(270deg)');
    }
    else {
        $("#preview").css("transform", 'rotate(0deg)');
    }
})
$(function () {
    $(".phone").mask('(999) 999-9999');
    $(".Alpha").bind("keypress", function (event) {
        // expression régulière pour accepter que les caractères alphabétiques
        let regExp = new RegExp("^[a-zA-Z-àÀâÂäÄçÇéÉèÈêÊëËìÌîÎïÏòÒôÔöÖùÙûÛüÜ '.''']+$");
        // saisir le caractère
        let key = String.fromCharCode(event.which);

        // si n'est pas un contrôle et backspace
        if ((event.which !== 0) && (event.which !== 8)) {
            // filtrer le caractère
            if (!regExp.test(key)) {
                // annuler la propagation de l'événement
                event.preventDefault();
            }
        }
    });
});
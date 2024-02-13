//document.addEventListener("DOMContentLoaded", function (event) {
//    const host = "https://localhost:7236";

//    document.querySelector(".start-button").addEventListener("click", function (e) {
//        e.preventDefault();

//        const player1Name = document.getElementById("player-name-1").value;

//        // Получите значения других полей, если они есть

//        const playerData = {
//            name: player1Name,
//            // Добавьте другие поля по вашему выбору
//        };

//        const xhr = new XMLHttpRequest();
//        xhr.open("POST", `${host}/player`, true);
//        xhr.setRequestHeader("Content-Type", "application/json");

//        xhr.onload = function () {
//            if (xhr.status === 201) {
//                console.log("Игрок успешно добавлен в базу данных.");
//                // Выполните дополнительные действия по успешному добавлению игрока
//            } else {
//                const error = JSON.parse(xhr.responseText);
//                console.error(`Ошибка: ${error.message}`);
//                // Обработайте ошибку со стороны сервера
//            }
//        };

//        xhr.send(JSON.stringify(playerData));
//    });
//});

document.addEventListener("DOMContentLoaded", function (event) {

    const host = "https://localhost:7236";

    $(".start-form").submit(function (e) {
        e.preventDefault();

        // Serialize both input fields
        const formData = $(".start-form").serializeArray();

        // Extract values from the serialized data
        const playerName1 = formData.find(item => item.name === "player_name_1").value;
        const playerName2 = formData.find(item => item.name === "player_name_2").value;

        // Create an array of objects to send both values
        const requestData = [
            { name: "player_name_1", value: playerName1 },
            { name: "player_name_2", value: playerName2 }
        ];

        $.ajax({
            url: `${host}/player`,
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify(requestData),
            success: function (data) {
                // Handle success if needed
            },
            error: function (jqXHR, textStatus, errorThrown) {
                const error = JSON.parse(jqXHR.responseText);
                const toast = $('#api-error-toast');

                $(toast).find('.toast-body').text(error.message);
                toast.toast("show");
            }
        });

        return false;
    });
});
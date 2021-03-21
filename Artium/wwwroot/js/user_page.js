function showProfileEditWindow() {

    var darkLayer = document.createElement('div'); // слой затемнения
    darkLayer.id = 'shadow'; // id чтобы подхватить стиль
    document.body.appendChild(darkLayer); // включаем затемнение
    document.body.style.overflow = "hidden";

    var modalWin = document.getElementById('profileEditWin'); // находим наше "окно"
    modalWin.style.display = 'block'; // "включаем" его


}

function closeEditWindow() {  

    var darkLayer = document.getElementById('shadow'); // слой затемнения
    darkLayer.parentNode.removeChild(darkLayer); // удаляем затемнение

    var modalWin = document.getElementById('profileEditWin'); // находим наше "окно"
    modalWin.style.display = 'none'; // делаем окно невидимым
    document.body.style.overflow = "auto";

    return false;
};

function newPostWindow() {

    var darkLayer = document.createElement('div'); // слой затемнения
    darkLayer.id = 'shadow'; // id чтобы подхватить стиль
    document.body.appendChild(darkLayer); // включаем затемнение
    document.body.style.overflow = "hidden";

    var modalWin = document.getElementById('newPostWin'); // находим наше "окно"
    modalWin.style.display = 'block'; // "включаем" его


}

function closeNewPostWin() {

    var darkLayer = document.getElementById('shadow'); // слой затемнения
    darkLayer.parentNode.removeChild(darkLayer); // удаляем затемнение

    var modalWin = document.getElementById('newPostWin'); // находим наше "окно"
    modalWin.style.display = 'none'; // делаем окно невидимым
    document.body.style.overflow = "auto";

    return false;
};
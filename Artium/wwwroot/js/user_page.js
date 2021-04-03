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

function newCommentWindow(postId) {
    
    var date = document.getElementById("post-date-" + postId).textContent;
    var text = document.getElementById("post-text-" + postId).textContent;
    var login = document.getElementById("post-login-" + postId).textContent;

    document.getElementById("commented-date").textContent = date;
    document.getElementById("commented-text").textContent = text;
    document.getElementById("commented-login").textContent = login;
    document.getElementById("commented-wallpostid").value = postId;

    var darkLayer = document.createElement('div'); // слой затемнения
    darkLayer.id = 'shadow'; // id чтобы подхватить стиль
    document.body.appendChild(darkLayer); // включаем затемнение
    document.body.style.overflow = "hidden";

    var modalWin = document.getElementById('newCommentWin'); // находим наше "окно"
    modalWin.style.display = 'block'; // "включаем" его
    event.stopPropagation();
}

function closeNewCommentWin() {

    var darkLayer = document.getElementById('shadow'); // слой затемнения
    darkLayer.parentNode.removeChild(darkLayer); // удаляем затемнение

    var modalWin = document.getElementById('newCommentWin'); // находим наше "окно"
    modalWin.style.display = 'none'; // делаем окно невидимым
    document.body.style.overflow = "auto";

    return false;
}
function goToPost(postId) {
    document.location = 'Post/' + postId; 
}
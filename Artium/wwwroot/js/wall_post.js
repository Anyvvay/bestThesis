function follow(userId, followerId) {
    $.ajax({
        url: '/User/Follow',
        type: 'POST',
        data: { 'userId': userId, 'followerId': followerId },
        success: function (data) {
            alert('Ок');
        },
        error: function (data) {
            alert('Ошибка при подписке');
        }
    });
}
function unfollow(userId, followerId) {
    $.ajax({
        url: '/User/Unfollow',
        type: 'POST',
        data: { 'userId': userId, 'followerId': followerId },
        success: function (data) {
            alert('Ок');
        },
        error: function (data) {
            alert('Ошибка при подписке');
        }
    });
}
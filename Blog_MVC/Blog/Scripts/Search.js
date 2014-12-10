function Search() {
    var query = $('#query').val();
    var searchby = parseInt($('#searchby').val());
    if (query != 0) {
        $.post("/Search/Search", { query: query, searchby: searchby }, function (data) {
            if (data != 0)
                $('#searchResults').empty().html(data);
            else
                $('#searchResults').empty().html('<div class="postContainer" style="margin-top: 20px;"><div class="postContent" style="padding-top:35px">Ничего не найдено</div></div>');
        });
    } else {
        setErrorInputMsg("query");
    }
}
function setErrorInputMsg(i) {
    $("#" + i).css('background', '#FDE0E0');
    $("#" + i).focus();
    setTimeout("$('#" + i + "').css('background', '#fff').focus()", 900);
}
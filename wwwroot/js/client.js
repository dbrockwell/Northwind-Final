$(function () {
    var toast = new Audio('/toast.wav');

    $('.code').on('click', function (e) {
        e.preventDefault();
        // first pause the audio (in case it is still playing)
        toast.pause();
        // reset the audio
        toast.currentTime = 0;
        // play audio
        toast.play();

        $('#product').html($(this).data("product"));
        $('#code').html($(this).data("code"));

        $('#toast').toast({ autohide: false }).toast('show');

    });
    
    var toastDisplayed = document.getElementById('toast')

    document.addEventListener('keydown', function (event) {

        if (event.key === 'Escape') {
            $(toastDisplayed).toast('hide');
        }
    })
});

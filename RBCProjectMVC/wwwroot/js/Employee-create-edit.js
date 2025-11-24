$(document).ready(function () {

    // Optional: preview selected file
    $('#File').on('change', function () {
        const file = this.files[0];
        if (file) {
            const fileName = file.name;
            const $label = $(this).prev('label');
            $label.text(`Selected file: ${fileName}`);
        }
    });

    // Form submit validation
    $('form').on('submit', function () {
        let isValid = true;

        $(this).find('input[required]').each(function () {
            if (!$(this).val()) {
                isValid = false;
                $(this).addClass('is-invalid');
            } else {
                $(this).removeClass('is-invalid');
            }
        });

        return isValid;
    });
});

$(function () {
            $('.tbl_type_1').each(function () {
                $(this).find('.tbl_item').last().css('border-bottom', '1px solid #ADADAD');
            });

            $('.tbl_type_1').each(function () {
                $(this).find('.bg_blue').last().css('border-bottom', '2px solid #ADADAD');
            });

            $('.tbl_type_2').each(function () {
                $(this).find('.bg_blue').last().css('border-right', '1px solid #ADADAD');
            });

            $('.tbl_type_2 .tbl_row').each(function (index, element) {
                $(element).find('.tbl_item').first().css('border-left', '1px solid #ADADAD');
            });
});
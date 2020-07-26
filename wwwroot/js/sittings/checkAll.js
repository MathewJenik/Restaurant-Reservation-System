$(document).ready(function () {

    //Area Switches

    $.each($('.checkArea'), (i, e) => {
        //initialize switch plugin
        $(e)[0].switchButton();

        let description = $(e).data('areadescription');
        let id = $(e).data('areaid');

        $(e).change((cv) => {
            $(`input[class*=${description}][type=checkbox]`).prop('checked', cv.currentTarget.checked);
            //check if all area switches are on (if so main switch on, if not main switch off)
            var totalAreas = parseInt($('#area-count').val());
            var checkedAreas = $('input[id*=checkAll_][type=checkbox]:checked').length; // -1 (extracting the main switch)
            var actionAreas = totalAreas === checkedAreas ? 'on' : 'off';
            $('#checkAll')[0].switchButton(actionAreas, true);
        });

        $(`[class*=${description}]`).change(() => {
            let allTablesHaveBeenSelected = $(`input[class*=${description}][type=checkbox]:checked`).length === $(`input[class*=${description}][type=checkbox]`).length;
            $(`#checkAll_${id}`)[0].switchButton(allTablesHaveBeenSelected ? 'on' : 'off', true);
        });
    });

    //Main switch

    //initialize switch plugin
    $('#checkAll')[0].switchButton();

    //updates state of the main switch and includes/excludes all tables
    $('#checkAll').change(function () {
        //update each table based on the state of the main switch (checked or not checked)
        $('input:checkbox').not(this).prop('checked', this.checked);
        //update each area switch (either on or off to reflect the state of the main switch)
        $.each($('input[id*="checkAll_"]'), (i, e) => {
            e.switchButton(this.checked ? 'on' : 'off', true);
        });
    });

    //updates main switch state when a table is included or excluded 
    $("input[id*=Tables_][type=checkbox]").change(function () {
        var total = parseInt($('#table-count').val());// $('input[id*=Tables_][type=checkbox]').length; 
        var checked = $('input[id*=Tables_][type=checkbox]:checked').length;
        var action = total === checked ? 'on' : 'off';
        $('#checkAll')[0].switchButton(action, true);

    });

});



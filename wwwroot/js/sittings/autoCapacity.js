function calculateCapacity() {
    let checkedTables = $('input[id*=Tables_][type=checkbox]:checked');
    let capacity = 0;
    for (let i = 0; i < checkedTables.length; i++) {
        capacity += checkedTables[i].getAttribute("capacity") - 0;
    }
    return capacity;
}

function initialCapacity() {
    document.querySelector('input[class*=max]').value = calculateCapacity();
    document.querySelector('#Sitting_Capacity').value = calculateCapacity();
}

window.onload = initialCapacity();

$(document).ready(function () {
    $("input:checkbox").change(function () {
        $('input[class*=max]').val(calculateCapacity());
        $('#Sitting_Capacity').val(calculateCapacity());
    });
});
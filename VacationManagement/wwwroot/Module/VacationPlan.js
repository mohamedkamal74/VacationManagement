
$(document).ready(() => {
    GetVacationTypes();
});




FindEmployee = () => {
    if ($('#employeeName').val() == '') {

        $('#ddlEmployees').html(`<option value="">------!-------لا يوجد موظفين بهذا الاسم</option>`);
    } else {
        $.ajax({

            url: '/api/VacationPlans/' + $('#employeeName').val(),
            method: 'GET',
            cache: false,
            success: (data) => {
                let employee = '';
                employee += `<option value="">----- الاسم الذي تبحث عنه موجود منه (${data.length}) -----</option>`;
                for (x in data) {
                    employee += `<option value="${data[x].id}">${data[x].id}-${data[x].name}</option>`;

                }
                $('#ddlEmployees').html(employee);
            }
        });
    }

}

GetVacationTypes = () => {

    $.ajax({

        url: '/VacationPlans/GetVacationTypes',
        method: 'GET',
        cache: false,
        success: (result) => {
            let vacations = '';
            vacations += `<option value="">----- اختر نوع الاجازة (${result.length}) -----</option>`;
            for (x in result) {
                vacations += `<option value="${result[x].id}" style="color:#ffff; background-color:${result[x].backGroundColor};"> ${result[x].id}-${result[x].vacationName}========>(عدد ايام الخصم${result[x].numberDays})</option>`;

            }
            $('#ddlVacationType').html(vacations);
        }
    });
}
document.addEventListener('DOMContentLoaded', function () {
   /* $.ajax({
        url: `api/scheduler/Sittings`,
        method: 'GET',
        dataType: 'json',
        contentType: 'application/json',
        success: function (data) {
            loadCalendar(data);

        }
    });

    function loadCalendar(viewsData) {
        

    };*/
    var calendarEl = document.getElementById('calendar');

    var calendar = new FullCalendar.Calendar(calendarEl, {
        //to hide the LicenseKey
        schedulerLicenseKey: 'GPL-My-Project-Is-Open-Source',
        plugins: ['interaction', 'dayGrid', 'timeGrid', 'resourceTimeline'],
        /*now: now,*/
        editable: true,
        aspectRatio: 1.8,
        scrollTime: '00:00',
        timeZone: 'local',
        minTime: '06:00:00',
        maxTime: '24:00:00',
        selectable: true,
        nowIndicator: true,
        droppable: true,
        navLinks: true,
        eventLimit: true,
        handleWindowResize: true,
        refetchResourcesOnNavigate: true,
        header: {
            left: ' today prev,next',
            center: 'title',
            right: 'resourceTimelineDay, dayGridMonth',
        },
        defaultView: 'resourceTimelineDay',
        buttonText: {
            today: 'Today',
            month: 'Month',
            week: 'Week',
            day: 'Day',
            list: 'List',
            Breakfast: 'Breakfast',
            Lunch: 'Lunch',
            Dinner: 'Dinner',
            Event: 'Event',
        },

        // Buttons that 
        /* views: viewsData,*/

        resourceGroupField: 'area',
        eventResourceEditable: true,
        resourceColumns: [
            {
                labelText: 'Areas & Tables',
                field: 'title'
            },
            {
                labelText: '# of Seat',
                field: 'capacity'
            }
        ],
        refetchResourcesOnNavigate: true,

        // get tables that into resources, this is linked with api/scheduler/SittingEvent on tableId

        resources: 'api/scheduler/Tables',


        slotEventOverlap: false,
        eventStartEditable: false,

        // displaying reservations and the sitting background 
        eventSources: [
            `api/scheduler/Reservations`,
            `api/scheduler/SittingsEvent`,

        ],
        dateClick: function (info) {
            console.log('Date: ' + info.dateStr);
            console.log('Resource ID: ' + info.resource.id);
            console.log('EventBackground: ' + info.event.title);
            console.log('EventResourceId: ' + info.event.ResourceId);
            console.log('EventTableSittingId: ' + info.event.GroupId);

        },
        eventReceive: function (info) {
            console.log(info.view.evets);
        },

        eventClick: function (info) {
            var details = `Reservation: ${info.event.title}\nGuests: ${info.event.extendedProps.guests}\nNotes: ${info.event.extendedProps.notes}`;
            alert(details);
            console.log(details);

        },
        eventDrop: function (info) {


            var oldTable = info.oldResource;
            var newTable = parseInt(info.newResource.id);
            var resId = parseInt(info.event.id);
            var resStartDate = moment(info.event.start).format();
            var resEndDate = moment(info.event.end).format(); var param = { dropTableId: newTable, reservationId: resId, ResStart: resStartDate };

            console.log(oldTable, newTable, resStartDate, resEndDate);


            $.ajax({
                url: 'api/scheduler/assign-table',
                method: 'POST',
                contentType: 'application/json',
                data: JSON.stringify(
                    {
                        dropTableId: newTable,
                        reservationId: resId,
                        ResStart: resStartDate
                    }),
                dataType: 'json',
                success: function (data) {
                    var d = data;

                },
                error: function (data) {
                    var d = data;
                }
            });



        },

    });
    calendar.render();
    

});




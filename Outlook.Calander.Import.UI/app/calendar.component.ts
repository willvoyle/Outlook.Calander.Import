//Typings Issue
declare var moment: any;

import { Component, OnInit } from '@angular/core';

@Component({
    selector: 'calendar',
    templateUrl: './calendar.html'
})

export class CalendarComponent implements OnInit {

    ngOnInit() {
        $(document).ready(function () {
            $('#calendar').fullCalendar({
                header: {
                    left: 'prev,next today',
                    center: 'title',
                    right: 'month,agendaWeek,agendaDay'
                },
                defaultView: 'agendaWeek',
                minTime: "09:00:00",
                maxTime: "17:30:00",
                timeFormat: "H:mm",
                eventBackgroundColor: 'rgba(33,132,110, 0.7)',
                eventTextColor: 'black',
                height: 600,
                navLinks: true,
                selectable: true,
                selectHelper: true,
                allDaySlot: false,
                select: function (start, end) {
                    var eventData = {
                        title: 'Meeting',
                        start: start,
                        end: end
                    };
                    $('#calendar').fullCalendar('renderEvent', eventData, true); // stick? = true
                    //$('#calendar').fullCalendar('unselect');
                },
                eventSources: [
                    {
                        url: 'http://localhost:2866/api/outlook',
                        color: 'rgba(158, 158, 158, 0.7)',
                        textColor: 'black'
                    }
                ]
            });
        });
    }
}
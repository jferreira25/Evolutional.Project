import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { Notification } from 'src/app/common/models/notification';
import { Observable } from 'rxjs';
import { Router } from '@angular/router';
import { RouteStackService } from 'src/app/common/services/route-stack.service';

@Component({
  selector: 'app-notification-bar',
  templateUrl: './notification-bar.component.html',
  styleUrls: ['./notification-bar.component.css'],
  encapsulation: ViewEncapsulation.None
})
export class NotificationBarComponent implements OnInit {

  public notifications: Notification[] = [];

  constructor(
    private router: Router,
    private routeStack: RouteStackService
  ) { }

  ngOnInit() {
    this.listen();
  }

  public goToNotificationList(): void {
    this.routeStack.goTo(this.router, 'app/notifications');
  }

  public goToNotificationDetail(notificationId: string): void {
    this.routeStack.goTo(this.router, 'app/notifications/detail', notificationId);
  }

  private listen(): void {
    
  }
}

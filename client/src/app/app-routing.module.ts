//modules declaration
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

//component declaration
import { RegisterComponent } from './register/register.component';
import { SignupComponent } from './signup/signup.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { ProjectScreenComponent } from './project-screen/project-screen.component';
import { ProjectDetailComponent } from './project-screen/project-detail/project-detail.component';
import { FillDetailsComponent } from './project-screen/fill-details/fill-details.component';
import { ForgetPasswordComponent } from './forget-password/forget-password.component';
import { RegisterUserWithNewPasswordComponent } from './register-user-with-new-password/register-user-with-new-password.component';
import { InvitePeopleComponent } from "./invite-people/invite-people.component";
import { TeamsComponent } from './teams/teams.component';
import { EpicComponent} from './epic/epic.component';
import { BacklogComponent} from './backlog/backlog.component';
import { ReleasePlanComponent } from './release-plan/release-plan.component';
import { NewReleaseFillingDetailsComponent } from './release-plan/new-release-filling-details/new-release-filling-details.component';
import { SprintComponent } from './sprint/sprint.component';
import { ChecklistComponent } from './checklist/checklist.component';


// paths to all the respective pages
const routes: Routes = [
    { path: "", redirectTo: 'app-signup', pathMatch: "full" },
    { path: 'app-signup', component: SignupComponent },
    { path: 'app-register/:id', component: RegisterComponent }, 
    { path: 'app-forget-password', component: ForgetPasswordComponent },
    { path: 'app-register-user-with-new-password/:id', component: RegisterUserWithNewPasswordComponent },
    { path: 'app-dashboard', component: DashboardComponent,
    children:[
    { path:'',redirectTo:'project-screen', pathMatch:'full'},
    { path: 'project-screen', component:ProjectScreenComponent},
    { path:'app-invite-people/:id', component: InvitePeopleComponent},
    { path: 'project-detail', component:ProjectDetailComponent },
    { path:'fill-details/:id',component:FillDetailsComponent},
    { path:'app-teams/:id', component: TeamsComponent },
    { path:'epic/:id',component:EpicComponent},
    { path:'backlog/:id',component:BacklogComponent},
    { path:'releaseplan/:id',component:ReleasePlanComponent},
    { path:'newreleasedetail/:id',component:NewReleaseFillingDetailsComponent},
    { path:'app-sprint',component:SprintComponent},
    { path:'app-checklist/:id',component:ChecklistComponent}
    ]}
]
@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule],
})
export class AppRoutingModule { }
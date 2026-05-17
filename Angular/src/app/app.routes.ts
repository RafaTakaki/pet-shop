import { Routes } from '@angular/router';
import { LoginComponent } from './components/login-page/login/login.component';
import { CriarContaComponent } from './components/login-page/criar-conta/criar-conta.component';
import { RecuperarSenhaComponent } from './components/login-page/recuperar-senha/recuperar-senha.component';
import { CadastroPetComponent } from './components/Pets/cadastro-pet/cadastro-pet.component';
import { AuthGuard } from './auth.guard';
import { FrontPageComponent } from './components/front-page/front-page.component';
import { ListarMeusPetsComponent } from './components/Pets/listar-meus-pets/listar-meus-pets.component';
import { CadastroAgendamentoComponent } from './components/agendamento/cadastro-agendamento/cadastro-agendamento.component';
import { NotificacoesComponent } from './components/notificacoes/notificacoes.component';
import { DetalhesPetComponent } from './components/Pets/detalhes-pet/detalhes-pet.component';


export const routes: Routes = [
    {
        path: 'login',
        component: LoginComponent
    },
    {
        path: 'pet/:nome',
        component: DetalhesPetComponent
    },
    {
        path: 'criar-conta',
        component: CriarContaComponent
    },
    {
        path: 'recuperar-senha',
        component: RecuperarSenhaComponent
    },
    {
        path: 'cadastro-pet',
        component: CadastroPetComponent,
        canActivate: [AuthGuard]
    },
    {  
        path: 'front-page',
        component: FrontPageComponent,
        canActivate: [AuthGuard]
        
    },
    {
        path: 'meus-pets',
        component: ListarMeusPetsComponent,
        canActivate: [AuthGuard]
    },
    {
        
        path: 'cadastro-agendamento',
        component: CadastroAgendamentoComponent,
        canActivate: [AuthGuard]
    },
    {
        path: 'notificacoes',
        component: NotificacoesComponent,
        canActivate: [AuthGuard]
    },
    {
        path: '**',   
        redirectTo: 'front-page',
        
    }


];

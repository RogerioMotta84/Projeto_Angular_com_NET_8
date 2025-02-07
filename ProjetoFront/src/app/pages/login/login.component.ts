import { Component } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  username = '';
  password = '';
  errorMessage = '';
  successMessage = '';

  constructor(private authService: AuthService, private router: Router) { }

  onLogin() {
    // Limpar mensagens anteriores
    this.errorMessage = '';
    this.successMessage = '';

    if (!this.username || !this.password) {
      this.errorMessage = 'Preencha todos os campos';
      return;
    }

    this.authService.login(this.username, this.password).subscribe({
      next: (response) => {
        this.successMessage = response.message; // Exibe mensagem de sucesso
        this.errorMessage = ''; // Limpa mensagem de erro
        this.username = ''; // Limpa o campo de usuário
        this.password = ''; // Limpa o campo de senha
      },
      error: () => {
        this.errorMessage = 'Usuário ou senha inválidos'; // Exibe mensagem de erro
        this.successMessage = ''; // Limpa mensagem de sucesso
        this.username = ''; // Limpa o campo de usuário
        this.password = ''; // Limpa o campo de senha
      }
    });
  }

  navigateToRegister() {
    this.router.navigate(['/register']);
  }
}



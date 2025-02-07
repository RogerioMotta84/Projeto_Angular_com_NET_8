import { Component } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {
  username = '';
  password = '';
  errorMessage = '';
  successMessage = '';

  constructor(private authService: AuthService, private router: Router) { }

  onRegister() {
    // Limpar mensagens anteriores
    this.errorMessage = '';
    this.successMessage = '';

    if (!this.username || !this.password) {
      this.errorMessage = 'Preencha todos os campos';
      return;
    }

    this.authService.register(this.username, this.password).subscribe({
      next: (response) => {
        this.successMessage = response.message; // Exibe mensagem de sucesso
        this.errorMessage = ''; // Limpa mensagem de erro
        this.username = ''; // Limpa o campo de usuário
        this.password = ''; // Limpa o campo de senha
      },
      error: () => {
        this.errorMessage = 'Usuário já existe'; // Exibe mensagem de erro
        this.successMessage = ''; // Limpa mensagem de sucesso
        this.username = ''; // Limpa o campo de usuário
        this.password = ''; // Limpa o campo de senha
      }
    });
  }

  navigateToLogin() {
    this.router.navigate(['/login']); // Roteia de volta para a tela de login
  }
}

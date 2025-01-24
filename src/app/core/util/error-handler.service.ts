import { Injectable } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';

import { MessagesService } from 'src/app/shared/components/messages/messages.service';

@Injectable({
  providedIn: 'root'
})
export class ErrorHandlerService {

  constructor(private messagesService: MessagesService) { }

  handle(errorResponse: any) {
    let msg: string;

    if (typeof errorResponse === 'string') {
      msg = errorResponse;
    } else if (errorResponse instanceof HttpErrorResponse
        && errorResponse.status >= 400 && errorResponse.status <= 499) {
      msg = 'Ocorreu um erro ao processar a sua solicitação';

      if (errorResponse.status === 403) {
        msg = 'Você não tem permissão para executar esta ação';
      }

      try {
        msg = errorResponse.error[0].mensagemUsuario;
      } catch (e) { }

      console.error('Ocorreu um erro', errorResponse);

    } else if (errorResponse.status === 404){
      msg = errorResponse.error.message;
    } else {
      msg = 'Erro ao processar serviço remoto. Tente novamente.';
      console.log('Ocorreu um erro', errorResponse);
    }
    this.messagesService.showError(msg);
  }
}

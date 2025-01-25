import { Injectable } from '@angular/core';
import { MessageService } from 'primeng/api';

@Injectable({
  providedIn: 'root'
})
export class MessagesService {

  constructor(private messageService: MessageService) {}

  public showSuccess(pDeDetail: string) {
    this.messageService.add({severity:'success', summary: 'Sucesso', detail: pDeDetail});
  }

  public showInfo(pDeDetail: string) {
    this.messageService.add({severity:'info', summary: 'Informações', detail: pDeDetail});
  }

  public showWarn(pDeDetail: string) {
    this.messageService.add({severity:'warn', summary: 'Aviso', detail: pDeDetail});
  }

  public showError(pDeDetail: string) {
    this.messageService.add({severity:'error', summary: 'Erro', detail: pDeDetail});
  }

  public showCustom(pDeDetail: string) {
    this.messageService.add({severity:'custom', summary: 'Customizada', detail: pDeDetail, icon: 'pi-file'});
  }

  public showConfirm(pTitle: string, pDetail: string) {
    this.messageService.clear();
    this.messageService.add({ key: 'c',sticky: true, severity:'warn', summary: pTitle, detail: pDetail});
  }

  public showMultiple(pMessages: any[]) {
    this.messageService.addAll(pMessages);
  }

  public onConfirm() {
    this.messageService.clear('c');
  }

  public onReject() {
    this.messageService.clear('c');
  }

  public clear() {
    this.messageService.clear();
  }
}

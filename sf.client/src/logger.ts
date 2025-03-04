import { Logger } from 'seq-logging';
import configData from "./logConfig.json";

class Log{

    logger: Logger;
    message: string;


    constructor(mes: string) {
        this.message = mes;
        this.logger = new Logger({
            serverUrl: configData.Seq.ServerUrl, 
            onError: (e) => {
                console.error('Failed to send log to Seq:', e);
            }
        });
    }
    info(){
        this.logger.emit({
            timestamp: new Date(),
            level: 'Information',
            messageTemplate: this.message,
            properties: { user: 'Justine' }
        });
        //this.logger.close();
    }
    warning(){
      this.logger.emit({
          timestamp: new Date(),
          level: 'warning',
          messageTemplate: this.message,
          properties: { user: 'Justine' }
      });
      //this.logger.close();
  }
  error(){
    this.logger.emit({
        timestamp: new Date(),
        level: 'error',
        // TODO: Get user
        messageTemplate: this.message,
        properties: { user: 'Justine' }
    });
    //this.logger.close();
}
}

export { Log };

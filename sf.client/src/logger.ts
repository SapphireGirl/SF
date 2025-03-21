import { Logger } from 'seq-logging';
import configData from "./logConfig.json";

class Log{

    logger: Logger;
    constructor() {
        this.logger = new Logger({
            serverUrl: configData.Seq.ServerUrl, 
            onError: (e) => {
                console.error('Failed to send log to Seq:', e);
            }
        });
    }
    info(mes: string){
        this.logger.emit({
            timestamp: new Date(),
            level: 'Information',
            messageTemplate: mes,
            properties: { user: 'Justine' }
        });
        //this.logger.close();
    }
    warning(mes: string){
      this.logger.emit({
          timestamp: new Date(),
          level: 'warning',
          messageTemplate: mes,
          properties: { user: 'Justine' }
      });
    }

    error(mes: string){
        this.logger.emit({
            timestamp: new Date(),
            level: 'error',
            // TODO: Get user
            messageTemplate: mes,
            properties: { user: 'Justine' }
        });
    
    }
}

export { Log };

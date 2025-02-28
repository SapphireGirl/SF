import { Logger } from 'seq-logging';

const logger = new Logger({
    serverUrl: 'http://localhost:5341', // Replace with your Seq server URL
    onError: (e) => {
        console.error('Failed to send log to Seq:', e);
    }

    });
      logger.emit({
        timestamp: new Date(),
        level: 'Information',
        traceId: 'frontend',
        messageTemplate: 'User {user} logged in',
        properties: { user: 'Justine' }

});

export { logger };

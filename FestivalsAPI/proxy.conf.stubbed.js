const PROXY_CONFIG = {
  '/codingtest/*': {
    'target': `http://localhost:9000`,
    'secure': false,
    'logLevel': 'debug',
  },
};

module.exports = PROXY_CONFIG;

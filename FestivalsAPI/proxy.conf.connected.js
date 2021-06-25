const PROXY_CONFIG = {
  '/codingtest/*': {
    'target': `https://eacp.energyaustralia.com.au/`,
    'secure': true,
	'changeOrigin': true,
    'logLevel': 'debug',
  },
};

module.exports = PROXY_CONFIG;

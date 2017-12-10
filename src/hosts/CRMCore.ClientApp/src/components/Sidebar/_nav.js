import { globalConfig } from "configs";

export default {
  items: [
    {
      name: 'Dashboard',
      url: '/dashboard',
      icon: 'icon-speedometer',
      badge: {
        variant: 'info'
      }
    },
    {
      name: 'CRM',
      icon: 'icon-credit-card',
      children: [
        {
          name: 'Calendar',
          url: '/crm/calendar',
          icon: 'icon-calendar',
          badge: {
            variant: 'info'
          }
        },
        {
          name: 'Tasks',
          url: '/crm/tasks',
          icon: 'icon-event',
          badge: {
            variant: 'info'
          }
        },
        {
          name: 'Campaigns',
          url: '/crm/campaigns',
          icon: 'icon-diamond',
          badge: {
            variant: 'info'
          }
        },
        {
          name: 'Leads',
          url: '/crm/leads',
          icon: 'icon-user',
          badge: {
            variant: 'info'
          }
        },
        {
          name: 'Accounts',
          url: '/crm/accounts',
          icon: 'icon-directions',
          badge: {
            variant: 'info',
            text: 'NEW'
          }
        },
        {
          name: 'Contacts',
          url: '/crm/contacts',
          icon: 'icon-directions',
          badge: {
            variant: 'info',
            text: 'NEW'
          }
        },
        {
          name: 'Opportunities',
          url: '/crm/opportunities',
          icon: 'icon-directions',
          badge: {
            variant: 'info',
            text: 'NEW'
          }
        },
        {
          name: 'Team',
          url: '/crm/team',
          icon: 'icon-directions',
          badge: {
            variant: 'info',
            text: 'NEW'
          }
        }
      ]
    },
    {
      name: 'Metadata',
      icon: 'icon-layers',
      children: [
        {
          name: 'Fields',
          url: '/metadata/fields',
          icon: 'icon-doc',
          badge: {
            variant: 'info'
          }
        },
        {
          name: 'Schemas',
          url: '/metadata/schemas',
          icon: 'icon-docs',
          badge: {
            variant: 'info'
          }
        }
      ]
    },
    {
      name: 'Pages',
      url: '/pages',
      icon: 'icon-star',
      children: [
        {
          name: 'Common',
          url: `${globalConfig.apiServer}/common`,
          icon: 'icon-star'
        },
        {
          name: 'Docs',
          url: `${globalConfig.apiServer}/docs`,
          icon: 'icon-star'
        },
        {
          name: 'Swagger',
          url: `${globalConfig.apiServer}/swagger`,
          icon: 'icon-star'
        },
        {
          name: 'Login',
          url: `${globalConfig.apiServer}/identity/account/login`,
          icon: 'icon-star'
        },
        {
          name: 'Register',
          url: '/register',
          icon: 'icon-star'
        },
        {
          name: 'Error 404',
          url: '/404',
          icon: 'icon-star'
        },
        {
          name: 'Error 500',
          url: '/500',
          icon: 'icon-star'
        }
      ]
    }
  ]
};
